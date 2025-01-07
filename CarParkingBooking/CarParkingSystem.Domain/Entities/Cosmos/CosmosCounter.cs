using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSystem.Domain.Entities.Cosmos
{
    public class CosmosCounter
    {
        public required string id { get; set; }  // Fixed ID for the counter document
        public int currentValue { get; set; }
        public string? PartitionId { get; set; }
    }
}
