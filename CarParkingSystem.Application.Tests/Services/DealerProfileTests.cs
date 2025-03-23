using AutoMapper;
using CarParkingSystem.Application.Services.DealerService;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;
using Moq;
namespace CarParkingSystem.Application.Tests.Services
{
    public class DealerProfileTests
    {
        private readonly Mock<IDealerRepository> _dealerRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IDealerSlotsRepository> _dealerSlotsRepository;
        private readonly Mock<IMapper> _mapper;
        private DealerProfile Sut;
        public DealerProfileTests()
        {
            _dealerRepository = new Mock<IDealerRepository>();
            _userRepository = new Mock<IUserRepository>();
            _dealerSlotsRepository = new Mock<IDealerSlotsRepository>();
            _mapper = new Mock<IMapper>();
            //Sut = new DealerProfile(_dealerRepository.Object, _mapper.Object, _userRepository.Object, _dealerSlotsRepository.Object);
        }

        #region DealerDashboard
        [Fact]
        public async Task DealerSignUp_WhenCalledWithValidData_ReturnsTrue()
        {
            //Arrange

        }
        #endregion
    }
}