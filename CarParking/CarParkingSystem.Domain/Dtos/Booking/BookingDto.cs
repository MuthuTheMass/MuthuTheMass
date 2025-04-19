using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;

namespace CarParkingSystem.Application.Dtos.Booking
{
    public class BookingDto
    {
        public string? DealerId { get; set; }

        public string? BookingId { get; set; }

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
        string? VehicleImage,
        string? BookingSource,
        DateTime? BookingFromDate,
        DateTime? BookingToDate,
        string? QrCode,
        string? AdvanceAmount,
        string? BookingStatus,
        string? AllottedSlots
    );

    public record UserBookingHistory(
        string BookingId,
        string VehicleNumber,
        DateTime? BookedDate,
        BookingProcessDetails Status,
        string SlotNumber,
        string ParkingName
        );
    
    public record PreUserBookingDetails(
        DateTime BookedDate,
        string BookingId,
        string CustomerName,
        string CustomerMobileNumber,
        string CustomerEmailId,
        string DealerName,
        string DealerStorename,
        string DealerEmail,
        string DealerMobileNumber,
        string DealerAddress,
        string VehicleNumber,
        string VehicleModal,
        string? VehicleDriverName,
        string? VehicleDriverMobileNumber,
        string  VehicleImage,
        string QrCode,
        string BookingStatus,
        string BookingSource,
        string AdvanceAmount,
        string AllotedSlots
        );

    public class CarBookingPdfDto
    {
        // Booking info
        public string BookingId { get; set; }
        public string PartitionId { get; set; }
        public string EncryptedBookingId { get; set; }
        public string DealerId { get; set; }
        public string BookingSource { get; set; }
        public string AllottedSlots { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        // Customer
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerProof { get; set; }
        public string CustomerProofNumber { get; set; }
        public string CustomerProofAuthority { get; set; }

        // Vehicle
        public string VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleImage { get; set; }

        // Dates
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? BookingDate { get; set; }

        // Payment
        public string TransactionId { get; set; }
        public string AdvanceAmount { get; set; }
        public string FinalAmount { get; set; }
        public string DueAmount { get; set; }
        public string CurrencyMode { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        // Status
        public string BookingState { get; set; }
        public string BookingReason { get; set; }

        // QR Code
        public byte[] QrCodeImage { get; set; }
    }

}