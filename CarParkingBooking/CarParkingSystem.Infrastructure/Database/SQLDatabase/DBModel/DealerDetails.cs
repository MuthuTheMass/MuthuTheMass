using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarParkingSystem.Infrastructure.Configurations;

namespace CarParkingSystem.Infrastructure.Database.SQLDatabase.DBModel
{
    public class DealerDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required string DealerID { get; set; }

        [DataType(DataType.Text)]
        public required string DealerName { get; set; }

        public byte[]? DealerProfilePicture { get; set; }

        public string? DealerStoreName { get; set; }

        [DataType(DataType.Text)]
        public required string DealerEmail { get; set; }

        [DataType(DataType.Text)]
        public required string DealerPhoneNo { get; set; }
        
        [DataType(DataType.Text)]
        public required string DealerPassword { get; set; }

        [DataType(DataType.MultilineText)]
        public string? DealerDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string? DealerTiming { get; set; }

        [DataType(DataType.MultilineText)]
        public string? DealerAddress { get; set; }

        [DataType(DataType.Text)]
        public string? DealerLandmark { get; set; }

        [DataType(DataType.Text)]
        public string? DealerGPSLocation { get; set; }

        [DataType(DataType.Text)]
        public string? DealerRating { get; set; }

        [DataType(DataType.Text)] 
        public string Rights { get; set; } = string.Empty; //AccessToUsers.Dealer.ToString();

        [DataType(DataType.Text)]
        public bool? DealerOpenOrClosed { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; } = DateTiming.GetIndianTime();

        [DataType(DataType.Text)]
        public bool IsValidUser { get; set; } = false;

    }
}
