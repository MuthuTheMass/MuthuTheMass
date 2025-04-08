using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSystem.Domain.Entities.SQL
{
    public class DealerSlotDetails
    {
        [Key] public required string Id { get; set; }

        public string? DealerId { get; set; }

        public string? EmailId { get; set; }

        public int Available_Slots { get; set; }

        public int Booked_Slots { get; set; }

        public int Total_Slots { get; set; }
    }
}