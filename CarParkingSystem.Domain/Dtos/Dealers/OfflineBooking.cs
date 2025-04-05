namespace CarParkingSystem.Domain.Dtos.Dealers
{
    public class OfflineBooking
    {
        public string? DealerId { get; set; }
        public string? DealerEmailId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerMobileNumber { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerProof { get; set; }
        public string? VehicleId { get; set; }
        public string? VehicleNumber { get; set; }
        public string? VehicleModel { get; set; }
        public string? bookingSource { get; set; }
    }
}