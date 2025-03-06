using CarParkingSystem.Domain.Dtos.Dealers;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
using CarParkingSystem.Infrastructure.Repositories;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CarParkingSystem.Infrastructure.Tests.Repository
{
    public class UserRepositoryTests
    {
        private readonly CarParkingBookingDbContext _dbContext;
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IDealerRepository> _dealerRepository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CarParkingBookingDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCarParkingDb")
            .Options;

            _dbContext = new CarParkingBookingDbContext(options);
            _bookingRepository = new Mock<IBookingRepository>();
            _dealerRepository = new Mock<IDealerRepository>();
        }

        #region GetUserDetailsForDealer

        [Fact]
        public async Task GetUserDetailsForDealer_ShouldReturnOrderedUserDetails()
        {
            // Arrange
            var testEmail = "dealer@example.com";
            var testDealer = TestDataFiller.TestDataFiller.FillTestData<DealerDetails>();

            var usersData = new List<UserDetailsNewCustomer>
        {
            new ("User1", new DateTime(2024, 1, 10)),
            new ("User2", new DateTime(2024, 1, 5)),  // Should appear first after sorting
            new ("User3", new DateTime(2024, 1, 15))
        };

            var userDetails =new List<UserDetails> 
                                 { TestDataFiller.TestDataFiller.FillTestData<UserDetails>(),
                                   TestDataFiller.TestDataFiller.FillTestData<UserDetails>(),
                                   TestDataFiller.TestDataFiller.FillTestData<UserDetails>(),
                                    };
            userDetails[0].UserID = "User1";
            userDetails[1].UserID = "User2";
            userDetails[2].UserID = "User3";
            userDetails[0].Name = "User One";
            userDetails[1].Name = "User Two";
            userDetails[2].Name = "User Three";

            _dealerRepository.Setup(repo => repo.GetUserByEmail(testEmail))
                           .ReturnsAsync(testDealer);

            _bookingRepository.Setup(repo => repo.GetUserByConfirmedBookingForDealer("D123"))
                            .ReturnsAsync(usersData);

            await _dbContext.UserDetails.AddRangeAsync(userDetails);
            await _dbContext.SaveChangesAsync();

            var service = new UserRepository(_dbContext, _bookingRepository.Object, _dealerRepository.Object);

            // Act
            var result = await service.GetUserDetailsForDealer(testEmail);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("User2", result[0].UserID); // User2 has the earliest date
            Assert.Equal("User1", result[1].UserID);
            Assert.Equal("User3", result[2].UserID);
        }

        #endregion
    }
}