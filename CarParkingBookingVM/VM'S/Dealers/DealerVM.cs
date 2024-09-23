
using System.Text.Json.Serialization;

namespace CarParkingBookingVM.VM_S.Dealers
{

    public class Filter
    {
        public string searchFrom { get; set; }

        public List<Filters> filters { get; set; }
    }

    public class Filters
    {
        public string  key { get; set; }
        public string value { get; set; }

        public string fullValue => $"{key} : {value}";
    }

    public class DealerVM
    {

        public string DealerName { get; set; }

        public string DealerEmail { get; set; }

        public string DealerPhoneNo { get; set; }

        public string DealerDescription { get; set; }

        public DateOnly? DealerStartDate { get; set; }

        public string DealerTiming { get; set; }

        public string DealerAddress { get; set; }

        public string DealerLandmark { get; set; }

        public GPSLocation DealerGPSLocation { get; set; }

        public string DealerRating { get; set; }

        private string DealerGPS
        {
            set { value = "" }
        }
    }

    public class GPSLocation
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
