using AutoMapper;
using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;

namespace CarParkingBooking.AutoMapper.Resolver
{
    public class BookingDetailsDtoResolver : ITypeConverter<CarBooking, CarBookingDetailDto>, ITypeConverter<CarBookingDetailDto, CarBooking>
    {
        public CarBookingDetailDto Convert(CarBooking source, CarBookingDetailDto destination, ResolutionContext context)
        {
            return new CarBookingDetailDto(
                source.id,
                source.CustomerData?.CustomerId,
                source.CustomerData?.CustomerName,
                source.CustomerData?.CustomerEmail,
                source.CustomerData?.CustomerMobileNumber,
                source.VehicleInfo?.VehicleNumber,
                source.VehicleInfo?.VehicleModel,
                source.BookingSource,
                source.BookingDate?.From,
                source.BookingDate?.To,
                ConvertQrByteToPngString(source.GeneratedQrCode), // Convert QR Code
                source.AdvanceAmount,
                source.BookingStatus?.State.ToString() ?? "Unknown", // Handle Enum
                source.AllottedSlots
            );
        }

        public CarBooking Convert(CarBookingDetailDto source, CarBooking destination, ResolutionContext context)
        {
            return new CarBooking
            {
                id = source.BookingId,
                CustomerData = new CustomerUserDetails
                {
                    CustomerId = source.CustomerId,
                    CustomerName = source.CustomerName,
                    CustomerEmail = source.CustomerEmail,
                    CustomerMobileNumber = source.CustomerPhoneNumber
                },
                VehicleInfo = new VehicleInformation
                {
                    VehicleNumber = source.VehicleNumber,
                    VehicleModel = source.VehicleModel
                },
                BookingSource = source.BookingSource,
                BookingDate = new CarBookingDates
                {
                    From = (DateTime)source.BookingFromDate,
                    To = source.BookingToDate
                },
                GeneratedQrCode = ConvertPngStringToQrByte(source.QrCode), // Convert back to Byte[]
                AdvanceAmount = source.AdvanceAmount,
                BookingStatus = new Status
                {
                    State = Enum.TryParse<BookingProcessDetails>(source.BookingStatus, out var state) ? state : BookingProcessDetails.Unknown
                },
                AllottedSlots = source.AllottedSlots
            };
        }

        private string? ConvertQrByteToPngString(byte[]? file)
        {
            return file is not null ? $"data:image/png;base64,{System.Convert.ToBase64String(file)}" : null;
        }

        private byte[]? ConvertPngStringToQrByte(string? base64String)
        {
            if (string.IsNullOrEmpty(base64String) || !base64String.StartsWith("data:image/png;base64,"))
                return null;

            try
            {
                return System.Convert.FromBase64String(base64String.Replace("data:image/png;base64,", ""));
            }
            catch
            {
                return null;
            }
        }
    }
}
