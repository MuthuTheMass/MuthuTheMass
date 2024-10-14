using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CarParkingBookingVM.VM_S.Vehicle
{
    public class VehicleVM
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
}
