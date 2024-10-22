using Microsoft.AspNetCore.Http;

namespace CarParkingBookingVM.VM_S
{
    public class UserUpdateDetails
    {
        public required string Name { get; set; }
        public required IFormFile ProfilePicture { get; set; }
        public required string Email { get; set; }
        public required string MobileNumber { get; set; }
        public required string Address { get; set; }
    }
}
