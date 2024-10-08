using AutoMapper;
using CarParkingBookingVM.VM_S.Booking;
using CarParkingBookingVM.VM_S.Dealers;
using Newtonsoft.Json;
using System.IO;
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

        public string convertFileToByte(IFormFile file)
        {
            ImageFile image;

            using (var steam = new MemoryStream())
            {
                file.CopyTo(steam);
                image = new()
                {
                    File = steam.ToArray(),
                    FileName = file.FileName,
                    ContentType = file.ContentType
                };
            }

            return JsonConvert.SerializeObject(image);
        }

        public IFormFile convertByteToFromFile(string file)
        {
            ImageFile? image = JsonConvert.DeserializeObject<ImageFile>(file);

            var stream = new MemoryStream(image!.File);

            IFormFile formFile = new FormFile(stream,0,image.File.Length,"file",image.FileName) 
            {
                Headers = new HeaderDictionary(),
                ContentType = image.ContentType
            };

            return formFile;
        }
    }
}
