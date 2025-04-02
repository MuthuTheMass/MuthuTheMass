using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;

namespace CarParkingSystem.Application.Dtos.Booking
{
    public class BookingDto
    {
        public string? DealerId { get; set; }

        public string? DealerEmail { get; set; }

        public string? CustomerId { get; set; }

        public CustomerDetails customerDetails { get; set; }

        public VehicleInformation? VehicleInfo { get; set; }

        public required BookingSources BookingSource { get; set; }

        public required CarBookingDates BookingDate { get; set; }

        public byte[]? GeneratedQrCode { get; set; }

        public string? AdvanceAmount { get; set; }

        public required Status BookingStatus { get; set; }
        public string? AllottedSlot { get; set; }
    }

    public class CustomerDetails
    {
        public string CustomerName { get; set; }
        public string? Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public Proof? Proof { get; set; }

    }

    public class Proof
    {
        public string? Type { get; set; }
        public string? Number { get; set; }
    }


    public record CarBookingDetailDto(
        string? BookingId,
        string? CustomerId,
        string? CustomerName,
        string? CustomerEmail,
        string? CustomerPhoneNumber,
        string? VehicleNumber,
        string? VehicleModel,
        string? BookingSource,
        DateTime? BookingFromDate,
        DateTime? BookingToDate,
        string? QrCode,
        string? AdvanceAmount,
        string? BookingStatus,
        string? AllottedSlots
        );
}
