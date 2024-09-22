using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CarParkingBookingDatabase.DBModel
{
    public class DealerDetails
    {
        [Key]
        public int DealerID { get; set; }

        [DataType(DataType.Text)]
        public string DealerName { get; set; }

        [DataType(DataType.Text)]
        public string DealerEmail { get; set; }

        [DataType(DataType.Text)]
        public string DealerPhoneNo { get; set; }

        [DataType(DataType.MultilineText)]
        public string DealerDescription { get; set; }

        [DataType(DataType.Date)]
        [AllowNull]
        public DateOnly? DealerStartDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string DealerTiming { get; set; }

        [DataType(DataType.MultilineText)]
        public string DealerAddress { get; set; }

        [DataType(DataType.Text)]
        public string DealerLandmark { get; set; }

        [DataType(DataType.Text)]
        public string DealerGPSLocation { get; set; }

        [DataType("Number")]
        public string DealerRating { get; set; }

    }
}
