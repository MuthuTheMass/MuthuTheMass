namespace CarParkingSystem.Application.Dtos.Booking
{
    public class BookingDto
    {
        public string User_ID { get; set; }
        public string Vehicle_Id { get; set; }
        public string Dealer_ID { get; set; }
        public string? Alternative_Number { get; set; }
        public string? Driver_Name { get; set; }
        public string? Driver_PhoneNumber { get; set; }
        public DateTime ArrivingTime { get; set; }
    }

    public class ImageFile
    {
        public byte[] File { get; set; }
        public string  FileName { get; set; }
        public string ContentType { get; set; }
    }
}
