using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSystem.Domain.Dtos.Dealers
{
    public record DealerSlotsRecord(string emailId, int availableSlots, int bookedSloots, int totalSlots)
    {
    }
}
