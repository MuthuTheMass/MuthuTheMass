using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CarParkingBookingVM.VM_S.Booking
{
    public class BookingVM
    {
        public string UserName { get; set; }
        public string Phone_Number { get; set; }
        public string Vehicle_Number { get; set; }
        public string Vehicle_Size_Type { get; set; }
        public string RC_Book_Number { get; set; }
        public IFormFile RC_Book_File { get; set; }
        public IFormFile Vehicle_Image { get; set; }
        public string Dealer_Name { get; set; }
        public string Dealer_PhoneNumber { get; set; }
        public string? Driver_Name { get; set; }
        public string Driver_PhoneNumber { get; set; }
        public DateTime ArrivingTime { get; set; }
    }

    public class ImageFile
    {
        public byte[] File { get; set; }
        public string  FileName { get; set; }
        public string ContentType { get; set; }
    }
}
