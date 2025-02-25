using CarParkingSystem.Domain.Helper;
using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Domain.Entities.SQL
{
    public class VehicleDetails
    {
        [Key]
        public string VehicleId { get; set; } = string.Empty;

        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public byte[] VehicleImage { get; set; }
        public string? DriverName { get; set; }
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? DriverPhoneNumber { get; set; }
        public string? VehicleModel { get; set; }
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? Alternative_Phone_Number { get; set; }
        //[ForeignKey("UserDetails")]
        public string UserID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; } = DateTiming.GetIndianTime();

        //public virtual UserDetails UserDetails { get; set; }


    }
}
