using Microsoft.AspNetCore.Http;

namespace CarParkingBookingVM.VM_S.Booking
{
    public class BookingVM
    {
        public UserDetails UserDetails { get; set; }

        public DateTime ArrivingDate { get; set; }

        public DateTime ExitDate { get; set; }


    }

    public class UserDetails
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class CarDetails
    {
        public string Name { get; set; }
        public string CarType { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleRC_Number { get; set; }
        public IFormFile VehicleRC_Image { get; set; }
    }
}
