using CarParkingBookingVM.CustomServices;
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
        public string User_ID { get; set; }
        [DataType(DataType.Text)]
        public string Vehicle_Id { get; set; }
        [DataType(DataType.Text)]
        public string Dealer_ID { get; set; }
        [DataType(DataType.Text)]
        public string? Driver_Name { get; set; }
        [DataType(DataType.Text)]
        public string Driver_PhoneNumber { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ArrivingTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; } = DateTiming.GetIndianTime();

        //public virtual ICollection<BookingTripDetails> BookingTripDetails { get; set; }

    }
}
