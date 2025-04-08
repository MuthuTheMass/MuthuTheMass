using CarParkingSystem.Domain.Entities.SQL;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext
{
    public class CarParkingBookingDbContext : DbContext
    {
        public CarParkingBookingDbContext(DbContextOptions<CarParkingBookingDbContext> options)
            : base(options)
        {
        }


        public DbSet<DealerDetails> DealerDetails { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<VehicleDetails> VehicleDetails { get; set; }
        public DbSet<DealerSlotDetails> DealerSlotDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDetails>().HasKey(b => b.UserID);
            modelBuilder.Entity<DealerDetails>().HasKey(b => b.DealerID);
            ;
            modelBuilder.Entity<VehicleDetails>(entity =>
            {
                // Configuring the primary key
                entity.HasKey(v => v.VehicleId);
            });
            ;
            modelBuilder.Entity<DealerSlotDetails>(entity =>
            {
                // Configuring the primary key
                entity.HasKey(v => v.Id);
            });
            ;
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

        #region custom_ids

        private async Task SetCustomIds()
        {
            var details =
                new (Type entityType, string prefix, Func<object, string> idGetter, Action<object, string> idSetter)[]
                {
                    (typeof(UserDetails), "User", u => ((UserDetails)u).UserID,
                        (u, id) => ((UserDetails)u).UserID = id),
                    (typeof(DealerDetails), "Dealer", d => ((DealerDetails)d).DealerID,
                        (d, id) => ((DealerDetails)d).DealerID = id),
                    (typeof(VehicleDetails), "Vehicle", v => ((VehicleDetails)v).VehicleId,
                        (v, id) => ((VehicleDetails)v).VehicleId = id),
                    (typeof(DealerSlotDetails), "Slots", v => ((DealerSlotDetails)v).Id,
                        (v, id) => ((DealerSlotDetails)v).Id = id),
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
                    .Where(id =>
                        !string.IsNullOrEmpty(id) && id.Contains('-')) // Ensure id is not null and contains '-'
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

        #endregion


        private static byte[] ByteArrayImage(string name)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net8.0\\", "");
            byte[] byteArray =
                System.IO.File.ReadAllBytes(Path.Combine(path, @".\Database\SQLDatabase\dummyData\" + name + ".jpg"));
            return byteArray;
        }

        #region Add dummy data for the first time

        public void SeedData()
        {
            int Total_Slots = 30;

            if (!UserDetails.Any(u => u.UserID.Equals("User-1")))
            {
                UserDetails.Add(new UserDetails()
                {
                    UserID = "User-1", Email = "balaji@gmail.com", Password = "balaji", Name = "balaji",
                    MobileNumber = "7896541235", UserProfilePicture = ByteArrayImage("user"),
                    Address = "dubai data, thiruvallur", Rights = "User"
                });
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
                    DealerAddress = "4723+W4X, Shanthi Nagar, Buckingham Carnatic Mills, Jamalia",
                    DealerCity = "Chennai",
                    DealerState = "Tamilnadu",
                    DealerCountry = "India",
                    DealerDescription = "Dealer description",
                    DealerGPSLocation =
                        "https://www.google.com/maps/place/SPR+City+Visitors+Car+Parking/@13.1023501,80.2338128,4234m/data=!3m1!1e3!4m10!1m2!2m1!1scar+parking!3m6!1s0x3a5265511637b7f5:0x351cd23e82728bc6!8m2!3d13.1023501!4d80.2528672!15sCgtjYXIgcGFya2luZ5IBC3BhcmtpbmdfbG904AEA!16s%2Fg%2F11swwvmlwl?authuser=0&entry=ttu&g_ep=EgoyMDI1MDEyOS4xIKXMDSoASAFQAw%3D%3D",
                    DealerLandmark = "SPR City",
                    DealerRating = "4.5",
                    DealerTiming =
                        "{\"monday\": {\"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"tuesday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"wednesday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"thursday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"friday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"saturday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"sunday\": { \"start\": \"06:54 AM\", \"stop\": \"05:45 PM\"},\"alwaysAvailable\": \"false\"}",
                    DealerStoreName = "SPR City Visitors Car Parking",
                    DealerOpenOrClosed = false,
                    DealerProfilePicture = ByteArrayImage("dealer"),
                    IsValidUser = true,
                    Rights = "Dealer",
                    OneHourAmount = 50
                });
                DealerDetails.Add(new DealerDetails()
                {
                    DealerID = "Dealer-2",
                    DealerName = "Prema",
                    DealerEmail = "prema@gmail.com",
                    DealerPassword = "prema",
                    DealerPhoneNo = "7894561235",
                    CreatedDate = new DateTime(2025, 02, 03),
                    DealerAddress =
                        "466Q+VR7, Sankara Mutt St, Neelam Garden, Bunder Garden, Perambur, Chennai, Tamil Nadu 600011",
                    DealerCity = "Chennai",
                    DealerState = "Tamilnadu",
                    DealerCountry = "India",
                    DealerDescription = "Dealer description",
                    DealerGPSLocation =
                        "https://www.google.com/maps/place/Prema+car+parking/@13.1116493,80.2341323,2117m/data=!3m1!1e3!4m10!1m2!2m1!1scar+parking!3m6!1s0x3a5265f7a6b3db63:0x2594f2ed66149724!8m2!3d13.1121501!4d80.2395447!15sCgtjYXIgcGFya2luZ5IBC3BhcmtpbmdfbG904AEA!16s%2Fg%2F11t7byz2tn?authuser=0&entry=ttu&g_ep=EgoyMDI1MDEyOS4xIKXMDSoASAFQAw%3D%3D",
                    DealerLandmark = "perambur railway statium",
                    DealerRating = "3",
                    DealerTiming =
                        "{\"monday\": {\"start\": \"06:54 AM\",\"stop\": \"05:45 PM\"},\"tuesday\": {\"start\": \"06:54 AM\",        \"stop\": \"05:45 PM\"    },    \"wednesday\": {        \"start\": \"06:54 AM\",        \"stop\": \"05:45 PM\"    },    \"thursday\": {        \"start\": \"06:54 AM\",        \"stop\": \"05:45 PM\"    },    \"friday\": {        \"start\": \"06:54 AM\",        \"stop\": \"05:45 PM\"    },    \"saturday\": {        \"start\": \"06:54 AM\",        \"stop\": \"05:45 PM\"    },    \"sunday\": {        \"start\": \"06:54 AM\",        \"stop\": \"05:45 PM\"    },    \"alwaysAvailable\": \"false\"}",
                    DealerStoreName = "Prema Car Parking",
                    DealerOpenOrClosed = false,
                    DealerProfilePicture = ByteArrayImage("OIP"),
                    IsValidUser = true,
                    Rights = "Dealer",
                    OneHourAmount = 50
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
                    VehicleImage = ByteArrayImage("OIP"),
                    UserID = "User-1",
                    VehicleName = "swift",
                    VehicleNumber = "TN 09 HR 9876"
                });
                SaveChanges();
            }

            if (!DealerSlotDetails.Any(u => u.Id.Equals("Slots-1")))
            {
                DealerSlotDetails.Add(new DealerSlotDetails()
                {
                    Id = "Slots-1",
                    DealerId = "Dealer-1",
                    EmailId = "surya@gmail.com",
                    Available_Slots = Total_Slots - 10,
                    Booked_Slots = Total_Slots - 20,
                    Total_Slots = Total_Slots,
                });
                SaveChanges();
            }
        }

        #endregion
    }
}