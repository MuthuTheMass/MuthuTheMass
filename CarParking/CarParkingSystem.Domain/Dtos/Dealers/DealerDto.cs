﻿using System.Globalization;

namespace CarParkingSystem.Domain.Dtos.Dealers
{
    public class Filter
    {
        public string searchFrom { get; set; }

        public List<Filters> filters { get; set; }

        public int pageNumber { get; set; }

        public int pageSize { get; set; }
    }

    public class Filters
    {
        public string key { get; set; }
        public string value { get; set; }

        public string fullValue => $"{key} : {value}";
    }

    public record DealerRecord(List<UserDealerSearch> Data, int TotalDataCount);


    public class DealerDto
    {
        public string? DealerId { get; set; }
        public required string DealerName { get; set; }

        public required string DealerEmail { get; set; }

        public required string DealerPhoneNo { get; set; }

        public required string DealerDescription { get; set; }

        public required Timing DealerTiming { get; set; }

        public required string DealerAddress { get; set; }

        public required string DealerLandmark { get; set; }
        public string? DealerCity { get; set; }
        public string? DealerState { get; set; }
        public string? DealerCountry { get; set; }

        public required string DealerStoreName { get; set; }

        public required string DealerLocationURL { get; set; }
        public bool DealerOpenOrClosed { get; set; }

        public required string DealerRating { get; set; }
        public string? OneHourAmount { get; set; }
        public string? Image { get; set; }

    }


    public class Timing
    {
        public moments? Monday { get; set; }
        public moments? Tuesday { get; set; }
        public moments? Wednesday { get; set; }
        public moments? Thursday { get; set; }
        public moments? Friday { get; set; }
        public moments? Saturday { get; set; }
        public moments? Sunday { get; set; }
        public string alwaysAvailable { get; set; } = string.Empty;
    }

    public class moments
    {
        private TimeOnly? _start;
        private TimeOnly? _stop;

        public string? Start
        {
            get => _start?.ToString("hh:mm tt");
            set => _start = value is not null ? Dtos_Helper.TimeConverter(value) : null;
        }

        public string? Stop
        {
            get => _stop?.ToString("hh:mm tt");
            set => _stop = value is not null ? Dtos_Helper.TimeConverter(value) : null;
        }
    }

    public class DeleteDealer
    {
        public string DealerName { get; set; } = string.Empty;

        public string DealerEmail { get; set; } = string.Empty;

        public string DealerPhoneNo { get; set; } = string.Empty;
    }

    public static class Dtos_Helper
    {
        public static TimeOnly? TimeConverter(string? timeString)
        {
            string[] formats = { "h:mm tt", "hh:mm tt", "h:mm t", "hh:mm t" };

            TimeOnly time;
            if (timeString is not null && TryParseTime(timeString, formats, out time))
            {
                return (time);
            }
            else
            {
                return (null);
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
    }
}