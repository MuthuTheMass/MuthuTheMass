namespace CarParkingSystem.Infrastructure.DtosHelper
{
        public class Filter
        {
            public string searchFrom { get; set; }

            public List<Filters> filters { get; set; }
        }

        public class Filters
        {
            public string key { get; set; }
            public string value { get; set; }

            public string fullValue => $"{key} : {value}";
        }
    
}
