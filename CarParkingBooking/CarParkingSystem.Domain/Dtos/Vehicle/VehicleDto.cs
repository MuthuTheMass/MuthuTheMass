using Microsoft.AspNetCore.Http;

namespace CarParkingSystem.Application.Dtos.Vehicle
{
    public class VehicleDto
    {
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public IFormFile VehicleImage { get; set; }
        public string? DriverName { get; set; }
        public string? DriverPhoneNumber { get; set; }
        public string? VehicleModel { get; set; }
        public string? Alternative_Phone_Number { get; set; }

    }

    public class Vehicle_User_VM
    {
        public string VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleImage { get; set; }
        public string? DriverName { get; set; }
        public string? DriverPhoneNumber { get; set; }
        public string? VehicleModel { get; set; }
        public string? Alternative_Phone_Number { get; set; }

    }

    public class Vehicle_Single_User_VM
    {
        public string VehicleId { get; set; }
        public string VehicleName { get; set;}
        public string VehicleNumber { get; set;}

    }
}
