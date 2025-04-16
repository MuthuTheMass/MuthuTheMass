using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Domain.Helper;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarParkingSystem.Application.Helper.DtoHelper
{
    public static class Dtos_Helper
    {
        public static TimeOnly? TimeConverter(string? timeString)
        {
            string[] formats = { "h:mm tt", "hh:mm tt", "h:mm t", "hh:mm t" };

            TimeOnly time;
            if (timeString is not null && TryParseTime(timeString, formats, out time))
            {
                return time;
            }
            else
            {
                return null;
            }
        }

        private static bool TryParseTime(string timeString, string[] formats, out TimeOnly result)
        {
            result = default;

            foreach (var format in formats)
            {
                if (TimeOnly.TryParseExact(timeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out result))
                {
                    return true;
                }
            }

            return false;
        }
        
        // Helper method to extract the number from "Booking-42"
        public static int ExtractBookingNumber(string bookingId)
        {
            if (string.IsNullOrWhiteSpace(bookingId))
                return 0;

            var parts = bookingId.Split('-');
            return parts.Length == 2 && int.TryParse(parts[1], out var number) ? number : 0;
        }

        public static PreUserBookingDetails? converter(CarBooking booking,VehicleDetails vehicle,DealerDetails dealer)
        {
           
            return new PreUserBookingDetails(
                booking.CreatedDate ?? DateTiming.GetIndianTime(),
                booking.id,
                booking.CustomerData.CustomerName,
                booking.CustomerData.CustomerMobileNumber,
                booking.CustomerData.CustomerEmail,
                dealer?.DealerName ?? "",
                dealer?.DealerStoreName ?? "",
                dealer?.DealerEmail ?? "",
                dealer?.DealerPhoneNo ?? "",
                dealer?.DealerAddress ?? "",
                booking.VehicleInfo.VehicleNumber,
                booking.VehicleInfo.VehicleModel,
                vehicle.DriverName,
                vehicle.DriverPhoneNumber,
                booking.VehicleInfo.VehicleImage,
                ConvertQrByteToPngString(booking.GeneratedQrCode),
                booking.BookingStatus.State.ToString(),
                booking.BookingSource.ToString(),
                booking?.Payment?.AdvanceAmount ?? "",
                booking?.AllottedSlots ?? "");
        }

        private static string? ConvertQrByteToPngString(byte[]? file)
        {
            return file is not null ? $"data:image/png;base64,{System.Convert.ToBase64String(file)}" : null;
        }
    }
}