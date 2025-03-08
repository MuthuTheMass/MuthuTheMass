using Microsoft.AspNetCore.Http;

namespace CarParkingSystem.Domain.Dtos.Dealers
{
    public class DashboardDetailsForDealer
    {
        public List<UserDetailsForDealer>? NewCustomers { get; set; }
        public int AvailableSlots { get; set; }
        public int BookedSlots { get; set; }
        public int TotalSlots { get; set; }
    }


    public class UserDetailsForDealer
    {
        public string? Name { get; set; }
        public IFormFile? Picture { get; set; }
        public string? MobileNumber { get; set; }
    }

    public record UserDetailsNewCustomer (string? Name,DateTime? DateTime);
}