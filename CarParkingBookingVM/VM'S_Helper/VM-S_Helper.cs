using System.Globalization;

namespace CarParkingBookingVM.VM_S_Helper
{
    public static class VM_S_Helper
    {
        public static TimeOnly? TimeConverter(string? timeString)
        {
            string[] formats = { "h:mm tt", "hh:mm tt", "h:mm t", "hh:mm t" };

            TimeOnly time;
            if (timeString is not null && TryParseTime(timeString, formats, out time))
            {
                return(time);
            }
            else
            {
                return(null);
            }
        }

        private static bool TryParseTime(string timeString, string[] formats, out TimeOnly result)
        {
            result = default;

            foreach (var format in formats)
            {
                if (TimeOnly.TryParseExact(timeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
