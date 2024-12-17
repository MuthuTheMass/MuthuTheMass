namespace CarParkingSystem.Infrastructure.Configurations
{
    public static class DateTiming
    {
        public static DateTime GetIndianTime()
        {
            TimeZoneInfo indianZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianZone);
        }
    }
}
