using CarParkingBookingDatabase.DBModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace CarParkingBookingDatabase.BookingDBContext
{
    // public class CarParkingBookingDBContext : IdentityDbContext<UserDetails, IdentityRole<int>, int>    
    public class CarParkingBookingDBContext : DbContext
    {
        public CarParkingBookingDBContext(DbContextOptions<CarParkingBookingDBContext> options)
        : base(options)
        {
        }


        public DbSet<BookingDetails> BookingDetails { get; set; }
        public DbSet<DealerDetails> DealerDetails { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<VehicleDetails> VehicleDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            //modelBuilder.Entity<IdentityUserRole<int>>().HasKey(r => new { r.UserId, r.RoleId });
            //modelBuilder.Entity<IdentityUserToken<int>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            modelBuilder.Entity<UserDetails>().HasKey(b => b.UserID);
            modelBuilder.Entity<DealerDetails>().HasKey(b => b.DealerID);
            modelBuilder.Entity<BookingDetails>().HasKey(b => b.BookingID);
            modelBuilder.Entity<VehicleDetails>(entity =>
            {
                // Configuring the primary key
                entity.HasKey(v => v.VehicleId);

                // Configuring the foreign key
                entity.HasOne(v => v.UserDetails) // Assuming VehicleDetails has a navigation property to User
                      .WithMany(u => u.VehicleDetails) // Assuming User has a collection of VehicleDetails
                      .HasForeignKey(v => v.UserID); // Foreign key property in VehicleDetails
            });
            base.OnModelCreating(modelBuilder);


        }

        public override int SaveChanges()
        {
            SetCustomIds().GetAwaiter().GetResult();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await SetCustomIds();
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private async Task GenerateIdsAsync(IEnumerable<object> entities, string prefix, Func<object, string> getId, Action<object, string> setId)
        {
            // Filter for 'Added' state entities in ChangeTracker
            var entries = ChangeTracker
                .Entries()
                .Where(e => entities.Contains(e.Entity) && e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList();

            // Determine the current max ID across all entities of this type (async)
            var maxId = entities
                .OfType<object>()
                .Select(getId)
                .Where(id => id != null)
                .OrderByDescending(id => int.Parse(id.Split('-')[1]))
                .FirstOrDefault();

            var currentIdNumber = maxId != null ? int.Parse(maxId.Split('-')[1]) : 0;

            // Assign new IDs to each entity
            foreach (var entity in entries)
            {
                setId(entity, $"{prefix}-{++currentIdNumber}");
            }
        }


        private async Task SetCustomIds()
        {
            var details = new (Type entityType, string prefix, Func<object, string> idGetter, Action<object, string> idSetter)[]
            {
                (typeof(UserDetails), "User", u => ((UserDetails)u).UserID, (u, id) => ((UserDetails)u).UserID = id),
                (typeof(DealerDetails), "Dealer", d => ((DealerDetails)d).DealerID, (d, id) => ((DealerDetails)d).DealerID = id),
                (typeof(BookingDetails), "Booking", b => ((BookingDetails)b).BookingID, (b, id) => ((BookingDetails)b).BookingID = id),
                (typeof(VehicleDetails), "Vehicle", v => ((VehicleDetails)v).VehicleId, (v, id) => ((VehicleDetails)v).VehicleId = id)
            };

            foreach (var (entityType, prefix, getId, setId) in details)
            {
                // Get new entries for this entity type
                var newEntries = ChangeTracker.Entries()
                    .Where(e => e.Entity.GetType() == entityType && e.State == EntityState.Added)
                    .Select(e => e.Entity)
                    .ToList();

                if (!newEntries.Any()) continue;

                // Use reflection to get the DbSet property for this entity type
                var dbSetProperty = this.GetType().GetProperty(entityType.Name);

                if (dbSetProperty == null) continue; // Skip if DbSet not found

                var dbSet = dbSetProperty.GetValue(this) as IQueryable<object>;
                if (dbSet == null) continue;

                // Fetch all entities from the DbSet as a list
                var entities = await dbSet.ToListAsync();

                // Get the current max ID from in-memory data with additional checks
                var maxId = entities
                    .Select(getId)
                    .Where(id => !string.IsNullOrEmpty(id) && id.Contains('-')) // Ensure id is not null and contains '-'
                    .Select(id =>
                    {
                        var parts = id.Split('-');
                        // Check if the split parts have the expected length
                        return parts.Length > 1 ? int.Parse(parts[1]) : 0; // Return 0 if invalid
                    })
                    .OrderByDescending(id => id)
                    .FirstOrDefault();

                var currentIdNumber = maxId;

                // Assign new IDs to each new entry
                foreach (var entity in newEntries)
                {
                    setId(entity, $"{prefix}-{++currentIdNumber}");
                }
            }
        }
    }
}
