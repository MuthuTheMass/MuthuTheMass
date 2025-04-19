using AutoMapper;
using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;

namespace CarParkingAPI.AutoMapper.Resolver
{
    public class BookingDetailsPDFDtoResolver : ITypeConverter<CarBooking, CarBookingPdfDto>, ITypeConverter<CarBookingPdfDto, CarBooking>
    {
        public CarBookingPdfDto Convert(CarBooking source, CarBookingPdfDto destination, ResolutionContext context)
        {
            return new CarBookingPdfDto
            {
                // Booking info
                BookingId = source.id,
                PartitionId = source.PartitionId ?? "",
                EncryptedBookingId = source.EncryptedBookingId,
                DealerId = source.DealerId ?? "",
                BookingSource = source.BookingSource,
                AllottedSlots = source.AllottedSlots ?? "Not Assigned",
                CreatedDate = source.CreatedDate,
                UpdatedDate = source.UpdatedDate,
                IsDeleted = source.IsDeleted,

                // Customer info
                CustomerName = source.CustomerData?.CustomerName,
                CustomerId = source.CustomerData?.CustomerId,
                CustomerEmail = source.CustomerData?.CustomerEmail,
                CustomerMobile = source.CustomerData?.CustomerMobileNumber,
                CustomerAddress = source.CustomerData?.CustomerAddress,
                CustomerProof = source.CustomerData?.CustomerProof ?? "N/A",
                CustomerProofNumber = source.CustomerData?.CustomerProofNumber ?? "N/A",
                CustomerProofAuthority = source.CustomerData?.CustomerAuthorityOfIssue ?? "N/A",

                // Vehicle info
                VehicleId = source.VehicleInfo?.VehicleId,
                VehicleNumber = source.VehicleInfo?.VehicleNumber,
                VehicleModel = source.VehicleInfo?.VehicleModel,
                VehicleImage = source.VehicleInfo?.VehicleImage,

                // Dates
                FromDate = source.BookingDate?.From,
                ToDate = source.BookingDate?.To,
                BookingDate = source.BookingDate?.UserBookingDate,

                // Payment info
                TransactionId = source.Payment?.TransactionId ?? "N/A",
                AdvanceAmount = source.Payment?.AdvanceAmount ?? "0",
                FinalAmount = source.Payment?.Final_Amount ?? "0",
                DueAmount = source.Payment?.Due_Amount ?? "0",
                CurrencyMode = source.Payment?.CurrencyMode?.ToString() ?? "N/A",
                PaymentMethod = source.Payment?.PaymentMethod?.ToString() ?? "N/A",
                PaymentStatus = source.Payment?.status?.ToString() ?? "N/A",

                // Status
                BookingState = source.BookingStatus?.State.ToString() ?? "Unknown",
                BookingReason = source.BookingStatus?.Reason ?? "N/A",

                // QR Code
                QrCodeImage = source.GeneratedQrCode ?? Array.Empty<byte>()
            };
        }

        // Convert CarBookingPdfDto to CarBooking
        public CarBooking Convert(CarBookingPdfDto source, CarBooking destination, ResolutionContext context)
        {
            return new CarBooking
            {
                id = source.BookingId,
                PartitionId = source.PartitionId,
                EncryptedBookingId = source.EncryptedBookingId,
                DealerId = source.DealerId,
                BookingSource = source.BookingSource,
                AllottedSlots = source.AllottedSlots,
                CreatedDate = source.CreatedDate,
                UpdatedDate = source.UpdatedDate,
                IsDeleted = source.IsDeleted,

                CustomerData = new CustomerUserDetails
                {
                    CustomerId = source.CustomerId,
                    CustomerName = source.CustomerName,
                    CustomerEmail = source.CustomerEmail,
                    CustomerMobileNumber = source.CustomerMobile,
                    CustomerAddress = source.CustomerAddress,
                    CustomerProof = source.CustomerProof,
                    CustomerProofNumber = source.CustomerProofNumber,
                    CustomerAuthorityOfIssue = source.CustomerProofAuthority
                },

                VehicleInfo = new VehicleInformation
                {
                    VehicleId = source.VehicleId,
                    VehicleNumber = source.VehicleNumber,
                    VehicleModel = source.VehicleModel,
                    VehicleImage = source.VehicleImage
                },

                BookingDate = new CarBookingDates
                {
                    From = source.FromDate,
                    To = source.ToDate,
                    UserBookingDate = source.BookingDate
                },

                Payment = new PaymentInfo
                {
                    TransactionId = source.TransactionId,
                    AdvanceAmount = source.AdvanceAmount,
                    Final_Amount = source.FinalAmount,
                    Due_Amount = source.DueAmount,
                    CurrencyMode = Enum.TryParse<Currency>(source.CurrencyMode, out var currency) ? currency : default,
                    PaymentMethod = Enum.TryParse<modeOfPayment>(source.PaymentMethod, out var paymentMethod) ? paymentMethod : default,
                    status = Enum.TryParse<BookingStatus>(source.PaymentStatus, out var status) ? status : default
                },

                BookingStatus = new Status
                {
                    State = Enum.TryParse<BookingProcessDetails>(source.BookingState, out var state)
                        ? state
                        : BookingProcessDetails.Unknown,
                    Reason = source.BookingReason
                },

                GeneratedQrCode = source.QrCodeImage
            };
        }
    }
}
