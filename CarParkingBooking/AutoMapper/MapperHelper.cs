using AutoMapper;
using CarParkingBookingVM.VM_S.Dealers;
using Newtonsoft.Json;
using System.Text.Json;

namespace CarParkingBooking.AutoMapper
{
    public class MapperHelper : Profile
    {
        public string convertstring(GPSLocation GPS)
        {
            return $"Latitude : {GPS.Latitude} , Longitude : {GPS.Longitude}";
        }

        public GPSLocation convertGPS(string GPS)
        {
            var latitude = GPS.Split(',').FirstOrDefault()?.Split(":").LastOrDefault()?.Trim();
            var longitude = GPS.Split(',').LastOrDefault()?.Split(":").LastOrDefault()?.Trim();

            return new GPSLocation()
            {
                Latitude = latitude ?? "",
                Longitude = longitude ?? ""
            } ;
        }
    }
}
