namespace CarParkingSystem.Domain.Helper
{
    public static class DateTiming
    {
        public static DateTime GetIndianTime()
        {
            TimeZoneInfo indianZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianZone);
        }

        public static DateTime GetIndianTime(string date)
        {
            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                TimeZoneInfo indianZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                return TimeZoneInfo.ConvertTimeToUtc(parsedDate, indianZone);
            }
            else
            {
                throw new ArgumentException("Invalid date format", nameof(date));
            }
        }


    }
}
