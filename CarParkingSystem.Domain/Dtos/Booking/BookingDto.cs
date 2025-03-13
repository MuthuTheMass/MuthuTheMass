using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;

namespace CarParkingSystem.Application.Dtos.Booking
{
    public class BookingDto
    {
        public string? DealerId { get; set; }

        public string? CustomerId { get; set; }

        public VehicleDetails? VehicleInfo { get; set; }

        public required BookingSources BookingSource { get; set; }

        public required CarBookingDates BookingDate { get; set; }

        public string? GeneratedQrCode { get; set; }

        public string? AdvanceAmount { get; set; }

        public required Status BookingStatus { get; set; }
        public string? AllottedSlots { get; set; }
    }
}
