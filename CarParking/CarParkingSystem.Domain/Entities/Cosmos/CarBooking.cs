using CarParkingSystem.Domain.ValueObjects;

namespace CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities
{
    public class CarBooking()
    {
        //BookingId
        public string id { get; set; }

        // id-dealerid-customerid
        public string? PartitionId { get; set; }
        public string EncryptedBookingId { get; set; }

        public string? DealerId { get; set; }

        public required CustomerUserDetails CustomerData { get; set; }

        public required VehicleInformation VehicleInfo { get; set; }

        public required string BookingSource { get; set; }

        public required CarBookingDates BookingDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public byte[]? GeneratedQrCode { get; set; }

        public string? AdvanceAmount { get; set; }

        public required Status BookingStatus { get; set; }

        public string? AllottedSlots { get; set; }
    }

    public class CarBookingDates
    {
        public DateTime? UserBookingDate { get; set; }
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }

    public class Status
    {
        public BookingProcessDetails State { get; set; }
        public string? Reason { get; set; }
    }

    public class VehicleInformation
    {
        public VehicleInformation()
        {
            VehicleId = string.Empty;
            VehicleNumber = string.Empty;
            VehicleModel = string.Empty;
        }

        public string VehicleId { get; set; }

        public string VehicleNumber { get; set; }

        public string VehicleModel { get; set; }
        public string VehicleImage { get; set; }
    }

    public class CustomerUserDetails
    {
        public CustomerUserDetails()
        {
            CustomerName = string.Empty;
            CustomerId = string.Empty;
            CustomerEmail = string.Empty;
            CustomerMobileNumber = string.Empty;
            CustomerAddress = string.Empty;
        }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobileNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string? CustomerProof { get; set; }
        public string? CustomerProofNumber { get; set; }
        public string? CustomerAuthorityOfIssue { get; set; }
    }
}