using CarParkingBookingVM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingBookingDatabase.DBModel
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DataType(DataType.Text)]
        public required string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public required string MobileNumber { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [DataType(DataType.Text)]
        public string Rights { get; set; } = AccessToUsers.User.ToString();
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }
        [DataType(DataType.Text)]
        //public string? RC_Book_Number { get; set; }
        //[DataType(DataType.Custom)]
        //public byte[]? RC_Book_Image { get; set; }
        //[DataType(DataType.Text)]
        //public string? Owner_Name { get; set; }
        //[DataType(DataType.PhoneNumber)]
        //public string? Owner_PhoneNo { get; set; }
        public string? VehicleNumber { get; set; }
    }
}
