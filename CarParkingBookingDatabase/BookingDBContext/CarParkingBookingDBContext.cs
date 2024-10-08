using CarParkingBookingDatabase.DBModel;
using Microsoft.EntityFrameworkCore;

namespace CarParkingBookingDatabase.BookingDBContext
{
    // public class CarParkingBookingDBContext : IdentityDbContext<UserDetails, IdentityRole<int>, int>    
    public class CarParkingBookingDBContext : DbContext
        {
        public CarParkingBookingDBContext(DbContextOptions<CarParkingBookingDBContext> options)
        : base(options)
        {
        }


        public DbSet<BookingDetails> bookingDetials { get; set; }
        public DbSet<DealerDetails> dealerDetails { get; set; }
        public DbSet<UserDetails> userDetails { get; set; }
        public DbSet<VehicleDetails> vehicleDetails { get; set; }


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
            var details = new (IEnumerable<object> list, string prefix, Func<object, string> idGetter, Action<object, string> idSetter)[]
                        {
                            (userDetails, "User", u => ((UserDetails)u).UserID, (u, id) => ((UserDetails)u).UserID = id),
                            (dealerDetails, "Dealer", d => ((DealerDetails)d).DealerID, (d, id) => ((DealerDetails)d).DealerID = id),
                            (bookingDetials, "Booking", b => ((BookingDetails)b).BookingID, (b, id) => ((BookingDetails)b).BookingID = id)
                        };

            foreach (var (list, prefix, idGetter, idSetter) in details)
            {
                GenerateIds(list, prefix, idGetter, idSetter);
            }


            return base.SaveChanges();
        }


        private void GenerateIds(IEnumerable<object> entities, string prefix, Func<object, string> getId, Action<object, string> setId)
        {
            // Filter the entities for those in 'Added' state
            var entries = ChangeTracker
                .Entries()
                .Where(e => entities.Contains(e.Entity) && e.State == EntityState.Added)
                .Select(e => e.Entity);

            // Determine the current max ID across all entities of this type
            var maxId = entities
                .OfType<object>()
                .Select(getId)
                .Where(id => id != null)
                .OrderByDescending(id => int.Parse(id.Split('-')[1]))
                .FirstOrDefault();

            var currentIdNumber = maxId != null ? int.Parse(maxId.Split('-')[1]) : 0;

            // Assign new IDs
            foreach (var entity in entries)
            {
                setId(entity, $"{prefix}-{++currentIdNumber}");
            }
        }

    }
}
