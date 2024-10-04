using CarParkingBookingDatabase.DBModel;
using Microsoft.EntityFrameworkCore;

namespace Auth.DataAccess.AuthDbContext
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
        }

        public DbSet<UserDetails> userDetails { get; set; }
        public DbSet<DealerDetails> dealerDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetails>().HasKey(b => b.UserID);

            modelBuilder.Entity<DealerDetails>().HasKey(b => b.DealerID);
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

            return base.SaveChanges();
        }
    }
}
