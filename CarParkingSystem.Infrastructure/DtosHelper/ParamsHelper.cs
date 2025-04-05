using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Domain.Entities.SQL;

namespace CarParkingSystem.Infrastructure.DtosHelper
{
    public class Filter
    {
        public string searchFrom { get; set; }

        public List<Filters> filters { get; set; }

        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }

    public class Filters
    {
        public string key { get; set; }
        public string value { get; set; }

        public string fullValue => $"{key} : {value}";
    }

    public record DealerRecord(List<DealerDetails> Data, int TotalDataCount);
}