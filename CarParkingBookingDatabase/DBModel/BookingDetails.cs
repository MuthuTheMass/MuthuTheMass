using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingBookingDatabase.DBModel
{
    public class BookingDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BookingID { get; set; }
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone_Number { get; set; }
        [DataType(DataType.Text)]
        public string Vehicle_Number { get; set; }
        [DataType(DataType.Text)]
        public string Vehicle_Size_Type { get; set; }
        [DataType(DataType.Text)]
        public string RC_Book_Number { get; set; }
        [DataType(DataType.Custom)]
        public string RC_Book_File { get; set; }
        [DataType(DataType.Custom)]
        public string Vehicle_Image { get; set; }
        [DataType(DataType.Text)]
        public string Dealer_Name { get; set; }
        [DataType(DataType.Text)]
        public string Dealer_PhoneNumber { get; set; }
        [DataType(DataType.Text)]
        public string? Driver_Name { get; set; }
        [DataType(DataType.Text)]
        public string Driver_PhoneNumber { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ArrivingTime { get; set; }
    }
}
