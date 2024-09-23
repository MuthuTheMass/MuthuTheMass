using AutoMapper;
using CarParkingBookingVM.VM_S.Dealers;
using Newtonsoft.Json;
using System.Text.Json;

namespace CarParkingBooking.AutoMapper
{
    public class MapperHelper : Profile
    {
        public string convert(GPSLocation GPS)
        {
            return JsonConvert.SerializeObject(GPS);
        }
    }
}
