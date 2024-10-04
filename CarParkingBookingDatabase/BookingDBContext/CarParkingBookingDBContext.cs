using CarParkingBookingDatabase.DBModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            //modelBuilder.Entity<IdentityUserRole<int>>().HasKey(r => new { r.UserId, r.RoleId });
            //modelBuilder.Entity<IdentityUserToken<int>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            modelBuilder.Entity<UserDetails>().HasKey(b => b.UserID);
            modelBuilder.Entity<DealerDetails>().HasKey(b => b.DealerID);
            modelBuilder.Entity<BookingDetails>().HasKey(b => b.BookingID);
            base.OnModelCreating(modelBuilder);


        }

        public override int SaveChanges()
        {

            var entries = ChangeTracker
               .Entries()
               .Where(e => e.Entity is UserDetails && e.State == EntityState.Added);

            foreach (var entityEntry in entries)
            {
                var userDetails = (UserDetails)entityEntry.Entity;
                var maxId = this.userDetails
                    .OrderByDescending(b => b.UserID)
                    .FirstOrDefault()?.UserID;

                var currentIdNumber = maxId != null ? int.Parse(maxId.Split('-')[1]) : 0;
                userDetails.UserID = $"User-{currentIdNumber + 1}";


            }

            var entrie = ChangeTracker
                .Entries()
                .Where(e => e.Entity is DealerDetails && e.State == EntityState.Added);

            foreach (var entityEntry in entrie)
            {
                var dealerDetails = (DealerDetails)entityEntry.Entity;
                var maxId = this.dealerDetails
                    .OrderByDescending(b => b.DealerID)
                    .FirstOrDefault()?.DealerID;

                var currentIdNumber = maxId != null ? int.Parse(maxId.Split('-')[1]) : 0;
                dealerDetails.DealerID = $"Dealer-{currentIdNumber + 1}";


            }


            var entrieses = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BookingDetails && e.State == EntityState.Added);

            foreach (var entityEntry in entrieses)
            {
                var bookingDetails = (BookingDetails)entityEntry.Entity;
                var maxId = this.bookingDetials
                    .OrderByDescending(b => b.BookingID)
                    .FirstOrDefault()?.BookingID;

                var currentIdNumber = maxId != null ? int.Parse(maxId.Split('-')[1]) : 0;
                bookingDetails.BookingID = $"Booking-{currentIdNumber + 1}";
            }

            return base.SaveChanges();
        }
    }
}
