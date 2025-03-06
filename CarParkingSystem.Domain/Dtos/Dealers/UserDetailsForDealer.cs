using Microsoft.AspNetCore.Http;

namespace CarParkingSystem.Domain.Dtos.Dealers
{
    public class UserDetailsForDealer
    {
        public string Name { get; set; }
        public IFormFile Picture { get; set; }
        public string MobileNumber { get; set; }
    }

    public record UserDetailsNewCustomer (string? Name,DateTime? DateTime);
}