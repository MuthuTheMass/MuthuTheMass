using CarParkingBookingDatabase.DBModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using CarParkingBookingDatabase.dummyData;

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
        public DbSet<BookingTripDetails> BookingTripDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDetails>().HasKey(b => b.UserID);
            modelBuilder.Entity<DealerDetails>().HasKey(b => b.DealerID);
            modelBuilder.Entity<BookingDetails>().HasKey(b => b.BookingID);
            modelBuilder.Entity<VehicleDetails>(entity =>
            {
                // Configuring the primary key
                entity.HasKey(v => v.VehicleId);

                //// Configuring the foreign key
                //entity.HasOne(v => v.UserDetails) // Assuming VehicleDetails has a navigation property to User
                //      .WithMany(u => u.VehicleDetails) // Assuming User has a collection of VehicleDetails
                //      .HasForeignKey(v => v.UserID); // Foreign key property in VehicleDetails
            });
            modelBuilder.Entity<BookingTripDetails>(entity =>
            {
                entity.HasKey(t => t.TripId);

                //entity.HasOne(o => o.BookingDetails)
                //      .WithMany(u => u.BookingTripDetails)
                //      .HasForeignKey(v => v.BookingID);
            });



            //if (!UserDetails.Any(u => u.UserID.Equals("User-1")))
            //{
            //    modelBuilder.Entity<UserDetails>().HasData(
            //    new UserDetails() { UserID = "User-1", Email = "balaji@gmail.com", Password = "balaji", Name = "balaji", MobileNumber = "7896541235" });
            //}
            //if (!DealerDetails.Any(u => u.DealerID.Equals("Dealer-1")))
            //{
            //    modelBuilder.Entity<DealerDetails>().HasData(
            //new DealerDetails()
            //{
            //    DealerID = "Dealer-1",
            //    DealerName = "surya",
            //    DealerEmail = "surya@gmail.com",
            //    DealerPassword = "surya",
            //    DealerPhoneNo = "5912364782",
            //    CreatedDate = new DateTime(2020, 05, 22),
            //    DealerAddress = "Address data",
            //    DealerDescription = "Dealer description",
            //    DealerGPSLocation = "URL",
            //    DealerLandmark = "Landmark",
            //    DealerRating = "3.3",
            //    DealerTiming = "{\"monday\": {\"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"tuesday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"wednesday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"thursday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"friday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"saturday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"sunday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"alwaysAvailable\": \"false\"}",
            //    DealerStoreName = "MuthuTheMass",
            //    DealerOpenOrClosed = false
            //});
            //}

            //if (!BookingDetails.Any(u => u.BookingID.Equals("Booking-1")))
            //{
            //    modelBuilder.Entity<BookingDetails>().HasData(
            //new BookingDetails()
            //{
            //    BookingID = "Booking-1",
            //    ArrivingTime = new DateTime().ToLocalTime(),
            //    CreatedDate = new DateTime().ToLocalTime().AddHours(-1),
            //    Dealer_ID = "Dealer-1",
            //    Driver_Name = "surya",
            //    Driver_PhoneNumber = "7536984126",
            //    User_ID = "User-1",
            //    Vehicle_Id = "Vehicle-1"
            //});
            //}
            //if (!VehicleDetails.Any(u => u.VehicleId.Equals("Vehicle-1")))
            //{
            //    modelBuilder.Entity<VehicleDetails>().HasData(
            //new VehicleDetails()
            //{
            //    VehicleId = "Vehicle-1",
            //    Alternative_Phone_Number = "7896321456",
            //    CreatedDate = new DateTime().ToLocalTime(),
            //    VehicleImage = byteArrayImage(),
            //    UserID = "User-1",
            //    VehicleName = "swift",
            //    VehicleNumber = "TN 09 HR 9876"
            //});
            //}


        }

        public override int SaveChanges()
        {
            SetCustomIds().GetAwaiter().GetResult();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await SetCustomIds();
            return await base.SaveChangesAsync(cancellationToken);
        }


        private async Task SetCustomIds()
        {
            var details = new (Type entityType, string prefix, Func<object, string> idGetter, Action<object, string> idSetter)[]
            {
                (typeof(UserDetails), "User", u => ((UserDetails)u).UserID, (u, id) => ((UserDetails)u).UserID = id),
                (typeof(DealerDetails), "Dealer", d => ((DealerDetails)d).DealerID, (d, id) => ((DealerDetails)d).DealerID = id),
                (typeof(BookingDetails), "Booking", b => ((BookingDetails)b).BookingID, (b, id) => ((BookingDetails)b).BookingID = id),
                (typeof(VehicleDetails), "Vehicle", v => ((VehicleDetails)v).VehicleId, (v, id) => ((VehicleDetails)v).VehicleId = id),
                (typeof(BookingTripDetails), "Trip", v => ((BookingTripDetails)v).TripId, (v, id) => ((BookingTripDetails)v).TripId = id)
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

        private static byte[] byteArrayImage()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\DatabaseMigrator\\bin\\Debug\\net8.0\\", "");
            byte[] byteArray = System.IO.File.ReadAllBytes(Path.Combine(path, @".\CarParkingBookingDatabase\dummyData\OIP.jpg"));
            return byteArray;
        }

        public void SeedData()
        {
            if (!UserDetails.Any(u => u.UserID.Equals("User-1")))
            {
                UserDetails.Add(new UserDetails() { UserID = "User-1", Email = "balaji@gmail.com", Password = "balaji", Name = "balaji", MobileNumber = "7896541235" });
                SaveChanges();
            }

            if (!DealerDetails.Any(u => u.DealerID.Equals("Dealer-1")))
            {
                DealerDetails.Add(new DealerDetails()
                {
                    DealerID = "Dealer-1",
                    DealerName = "surya",
                    DealerEmail = "surya@gmail.com",
                    DealerPassword = "surya",
                    DealerPhoneNo = "5912364782",
                    CreatedDate = new DateTime(2020, 05, 22),
                    DealerAddress = "Address data",
                    DealerDescription = "Dealer description",
                    DealerGPSLocation = "URL",
                    DealerLandmark = "Landmark",
                    DealerRating = "3.3",
                    DealerTiming = "{\"monday\": {\"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"tuesday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"wednesday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"thursday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"friday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"saturday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"sunday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"alwaysAvailable\": \"false\"}",
                    DealerStoreName = "MuthuTheMass",
                    DealerOpenOrClosed = false
                });
                SaveChanges();
            }

            if (!BookingDetails.Any(u => u.BookingID.Equals("Booking-1")))
            {
                BookingDetails.Add(new BookingDetails()
                {
                    BookingID = "Booking-1",
                    ArrivingTime = DateTime.Now,
                    CreatedDate = DateTime.Now.AddHours(-1),
                    Dealer_ID = "Dealer-1",
                    Driver_Name = "surya",
                    Driver_PhoneNumber = "7536984126",
                    User_ID = "User-1",
                    Vehicle_Id = "Vehicle-1"
                });
                SaveChanges();
            }

            if (!VehicleDetails.Any(u => u.VehicleId.Equals("Vehicle-1")))
            {
                VehicleDetails.Add(new VehicleDetails()
                {
                    VehicleId = "Vehicle-1",
                    Alternative_Phone_Number = "7896321456",
                    CreatedDate = DateTime.Now,
                    VehicleImage = byteArrayImage(),
                    UserID = "User-1",
                    VehicleName = "swift",
                    VehicleNumber = "TN 09 HR 9876"
                });
                SaveChanges();
            }
        }
    }
}
