using Microsoft.AspNetCore.Http;

namespace CarParkingBookingVM.VM_S
{
    public class UserData
    {
        public  string Name { get; set; }
        public  IFormFile ProfilePicture { get; set; }
        public  string Email { get; set; }
        public  string MobileNumber { get; set; }
        public  string Address { get; set; }
    }
    
    public class UserDataVM
    {
        public required string Name { get; set; }
        public required string ProfilePicture { get; set; }
        public required string Email { get; set; }
        public required string MobileNumber { get; set; }
        public required string Address { get; set; }
    }
}
