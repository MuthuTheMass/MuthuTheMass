using CarParkingBookingDatabase.DBModel;
using Microsoft.EntityFrameworkCore;

namespace CarParkingBookingDatabase.BookingDBContext
{
    public class CarParkingBookingDBContext : DbContext
    {
        public CarParkingBookingDBContext(DbContextOptions<CarParkingBookingDBContext> options)
        : base(options)
        {
        }


        public DbSet<UserDetails> userDetails {  get; set; }

        public DbSet<DealerDetails> dealerDetails { get; set; }

        public DbSet<BookingDetails> bookingDetials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetails>().HasKey(b => b.ID);

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
