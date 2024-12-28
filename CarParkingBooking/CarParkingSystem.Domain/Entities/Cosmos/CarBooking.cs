using CarParkingSystem.Domain.ValueObjects;

namespace CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities
{
    public class CarBooking
    {
        //BookingId
        public required string id { get; set; }

        // id-dealerid-customerid
        public string? PartitionId { get; set; }

        public string? DealerId { get; set; }

        public string? CustomerId { get; set; }

        public string? VehicleId { get; set; }

        public required string BookingSource { get; set; }

        public int MyProperty { get; set; }

        public required CarBookingDates BookingDate { get; set; }

        public string? CreatedDate { get; set; }

        public string? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string? GeneratedQrCode { get; set; }

        public string? AdvanceAmount { get; set; }

        public required Status BookingStatus { get; set; }
    }

    public class CarBookingDates
    {
        public required string From { get; set; }

        public required string To { get; set; }
    }

    public class Status
    {
        public BookingProcessDetails State { get; set; }
        public string? Reason { get; set; }
    }
}
