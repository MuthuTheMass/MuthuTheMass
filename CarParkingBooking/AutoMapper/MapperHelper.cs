using AutoMapper;
using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Domain.Dtos.Dealers;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using Newtonsoft.Json;

namespace CarParkingBooking.AutoMapper
{
    public class MapperHelper : Profile
    {
        //public string ConvertString(GPSLocation GPS)
        //{
        //    return JsonConvert.SerializeObject(GPS);
        //}

        //public GPSLocation ConvertGPS(string GPS)
        //{
        //    return JsonConvert.DeserializeObject<GPSLocation>(GPS);
        //}

        public string ConvertTimingString(Timing times)
        {
            var timing = JsonConvert.SerializeObject(times);
            return timing;
        }

        public Timing? ConvertStringTiming(string timing)
        {
            Timing? data = JsonConvert.DeserializeObject<Timing>(timing);
            return data;
        }

        //public string convertFileToByte(IFormFile file)
        //{
        //    ImageFile image;

        //    using (var steam = new MemoryStream())
        //    {
        //        file.CopyTo(steam);
        //        image = new()
        //        {
        //            File = steam.ToArray(),
        //            FileName = file.FileName,
        //            ContentType = file.ContentType
        //        };
        //    }

        //    return JsonConvert.SerializeObject(image);
        //}

        //public IFormFile convertByteToFromFile(string file)
        //{
        //    ImageFile? image = JsonConvert.DeserializeObject<ImageFile>(file);

        //    var stream = new MemoryStream(image!.File);

        //    IFormFile formFile = new FormFile(stream,0,image.File.Length,"file",image.FileName) 
        //    {
        //        Headers = new HeaderDictionary(),
        //        ContentType = image.ContentType
        //    };

        //    return formFile;
        //}

        public byte[] ConvertFileToByte(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null; // Handle the case where the file is null or empty
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public IFormFile ConvertByteToFromFile(byte[] file)
        {
            var stream = new MemoryStream(file);

            IFormFile formFile = new FormFile(stream, 0, file.Length, "file", "")
            {
                Headers = new HeaderDictionary(),
            };

            return formFile;
        }

        public string? ConvertQrByteToPngString(byte[] file)
        {
            if (file is null) return null;

            return $"data:image/png;base64,{Convert.ToBase64String(file)}";
        }

        public string? ConvertByteToString(byte[]? bytes)
        {
            if (bytes != null || bytes?.Length > 0)
            {
                return Convert.ToBase64String(bytes);
            }

            return null;
        }

        public async Task<string> ConvertIFormFileToBase64String(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                return $"data:image/jpeg;base64,{Convert.ToBase64String(fileBytes)}";
            }
        }

        public DateTime GetIndianTime()
        {
            TimeZoneInfo indianZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianZone);
        }

        public DateTime GetIndianTime(string date)
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