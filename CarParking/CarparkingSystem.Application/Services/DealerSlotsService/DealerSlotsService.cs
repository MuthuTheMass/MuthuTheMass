using CarParkingSystem.Domain.Dtos.Dealers;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSystem.Application.Services.DealerSlotsService
{
    public interface IDealeaSlotsService
    {
        Task<DealerSlotsRecord> GetSlotDataByEmailId(string emailId);
    }


    public class DealerSlotsService : IDealeaSlotsService
    {
        private readonly IDealerSlotsRepository _dealerSlotsRepository;

        public DealerSlotsService(IDealerSlotsRepository dealerSlotsRepository)
        {
            _dealerSlotsRepository = dealerSlotsRepository;
        }

        public async Task<DealerSlotsRecord?> GetSlotDataByEmailId(string? emailId)
        {
            if (string.IsNullOrWhiteSpace(emailId))
                return null;

            var dealerSlotDetails = await _dealerSlotsRepository.GetSlotsByDealerId(emailId);

            return new DealerSlotsRecord(dealerSlotDetails.DealerId, dealerSlotDetails.Available_Slots,
                dealerSlotDetails.Booked_Slots, dealerSlotDetails.Total_Slots);
        }
    }
}