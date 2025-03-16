using CarParkingSystem.Domain.ValueObjects;

namespace CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities
{
    public class CarBooking
    {
        //BookingId
        public string id { get; set; }

        // id-dealerid-customerid
        public string? PartitionId { get; set; }

        public string? DealerId { get; set; }

        public string? CustomerId { get; set; }

        public VehicleDetails? VehicleInfo { get; set; }

        public string BookingSource { get; set; }

        public CarBookingDates BookingDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string? GeneratedQrCode { get; set; }

        public string? AdvanceAmount { get; set; }

        public Status BookingStatus { get; set; }

        public string? AllottedSlots { get; set; }
    }

    public class CarBookingDates
    {
        public required DateTime From { get; set; }

        public DateTime? To { get; set; }
    }

    public class Status
    {
        public BookingProcessDetails State { get; set; }
        public string? Reason { get; set; }
    }

    public class VehicleDetails
    {
        public string VehicleId { get; set; }

        public string VehicleNumber { get; set; }
    }
}
