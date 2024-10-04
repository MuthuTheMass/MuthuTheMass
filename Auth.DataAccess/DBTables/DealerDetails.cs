//using CarParkingBookingVM.Enums;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Diagnostics.CodeAnalysis;

//namespace CarParkingBookingDatabase.DBModel
//{
//    public class DealerDetails
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public string DealerID { get; set; }

//        [DataType(DataType.Text)]
//        public string DealerName { get; set; }

//        [DataType(DataType.Text)]
//        public string DealerEmail { get; set; }

//        [DataType(DataType.Text)]
//        public string DealerPhoneNo { get; set; }

//        [DataType(DataType.MultilineText)]
//        public string DealerDescription { get; set; }

//        [DataType(DataType.Date)]
//        [AllowNull]
//        public DateTime? DealerStartDate { get; set; }

//        [DataType(DataType.MultilineText)]
//        public string DealerTiming { get; set; }

//        [DataType(DataType.MultilineText)]
//        public string DealerAddress { get; set; }

//        [DataType(DataType.Text)]
//        public string DealerLandmark { get; set; }

//        [DataType(DataType.Text)]
//        public string DealerGPSLocation { get; set; }

//        [DataType(DataType.Text)]
//        public string DealerRating { get; set; }

//        [DataType(DataType.Text)]
//        public string Rights { get; set; } = AccessToUsers.Dealer.ToString();

//    }
//}
