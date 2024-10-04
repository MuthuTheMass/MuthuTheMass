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

        public DbSet<DealerDetails> dealerDetails { get; set; }

        public DbSet<BookingDetails> bookingDetials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            //modelBuilder.Entity<IdentityUserRole<int>>().HasKey(r => new { r.UserId, r.RoleId });
            //modelBuilder.Entity<IdentityUserToken<int>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            modelBuilder.Entity<DealerDetails>().HasKey(b => b.DealerID);

            modelBuilder.Entity<BookingDetails>().HasKey(b => b.BookingID);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BookingDetails && e.State == EntityState.Added);

            foreach (var entityEntry in entries)
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
