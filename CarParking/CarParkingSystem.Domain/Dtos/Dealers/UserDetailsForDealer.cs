using Microsoft.AspNetCore.Http;

namespace CarParkingSystem.Domain.Dtos.Dealers
{
    public class DashboardDetailsForDealer
    {
        public List<UserDetailForDealer>? NewCustomers { get; set; }
        public int AvailableSlots { get; set; }
        public int BookedSlots { get; set; }
        public int TotalSlots { get; set; }
        public List<RecentBookingInDealerDashBoard>? RecentBookings { get; set; }
    }


    public class UserDetailsForDealer
    {
        public string? Name { get; set; }
        public IFormFile? Picture { get; set; }
        public string? MobileNumber { get; set; }
    }

    public record UserDetailForDealer(string? Name, string? Picture, string? MobileNumber);

    public record UserDetailsNewCustomer(string? Name, DateTime? DateTime);

    public record RecentBookingInDealerDashBoard(
        string BookingID,
        string VehicleNumber,
        DateTime? Date,
        string Status,
        string Slot_Number,
        byte[] QRCode
    );
}