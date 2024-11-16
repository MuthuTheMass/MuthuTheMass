using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseMigrator.DBModel
{
    public class BookingTripDetails
    {
        [Key]
        public string TripId { get; set; }
        public string TripName { get; set;}
        public string TripDescription { get; set; }
        public DateTime CreatedTime { get; set; }

        //[ForeignKey("BookingDetails")]
        public string BookingID { get; set; }

        //public virtual BookingDetails BookingDetails { get; set; }

    }
}
 