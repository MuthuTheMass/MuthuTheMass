using AutoMapper;
using CarParkingBookingVM.VM_S.Dealers;
using Newtonsoft.Json;
using System.Text.Json;

namespace CarParkingBooking.AutoMapper
{
    public class MapperHelper : Profile
    {
        public string ConvertString(GPSLocation GPS)
        {
            return JsonConvert.SerializeObject(GPS);
        }

        public GPSLocation ConvertGPS(string GPS)
        {
            return JsonConvert.DeserializeObject<GPSLocation>(GPS);
        }

        public string ConvertTimingString(Timing times)
        {
            var timing = Newtonsoft.Json.JsonConvert.SerializeObject(times);
            return timing;
        }

        public Timing ConvertStringTiming(string timing)
        {
            Timing data = JsonConvert.DeserializeObject<Timing>(timing);
            return data;

        }
    }
}
