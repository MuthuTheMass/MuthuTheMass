using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Infrastructure.Repositories.SQL_Repository
{
    public interface IDealerSlotsRepository
    {
        Task <DealerSlotDetails?> GetSlotsByDealerId(string dealerId);

        Task<List<DealerSlotDetails>> Get();
        
        Task<bool> UpsertDealerSlots(DealerSlotDetails dealerSlotDetails);

        Task<bool> DeleteDealerSlotsByDealerId(string dealerId);
    }

    public class DealerSlotsRepository : IDealerSlotsRepository
    {
        private readonly CarParkingBookingDbContext _dbContext;

        public DealerSlotsRepository(CarParkingBookingDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteDealerSlotsByDealerId(string dealerId)
        {
            if(string.IsNullOrEmpty(dealerId))
                return false;

            var dealerSlotDetails = await _dbContext.DealerSlotDetails.FirstOrDefaultAsync(d => d.DealerId == dealerId);
            if (dealerSlotDetails == null)
                return false;

            _dbContext.DealerSlotDetails.Remove(dealerSlotDetails);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<DealerSlotDetails>> Get()
        {
            List<DealerSlotDetails> dealerSlotDetails = await _dbContext.DealerSlotDetails.ToListAsync();
            if(dealerSlotDetails == null || dealerSlotDetails.Count <=0)
                return new List<DealerSlotDetails>();
            return dealerSlotDetails;
        }

        public async Task<DealerSlotDetails?> GetSlotsByDealerId(string dealerId)
        {
            if(string.IsNullOrEmpty(dealerId))
                return null;

            DealerSlotDetails? dealerSlotDetails = await _dbContext.DealerSlotDetails.FirstOrDefaultAsync(d => d.DealerId == dealerId);

            if(dealerSlotDetails == null)
                return new DealerSlotDetails() { Id ="none" };

            return dealerSlotDetails;

        }

        public async Task<bool> UpsertDealerSlots(DealerSlotDetails dealerSlotDetails)
        {
            if (dealerSlotDetails == null)
                return false;

            var dealerSlot = await _dbContext.DealerSlotDetails.FirstOrDefaultAsync(d => d.DealerId == dealerSlotDetails.DealerId);
            if (dealerSlot == null)
            {
                await _dbContext.DealerSlotDetails.AddAsync(dealerSlotDetails);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                dealerSlot.Available_Slots = dealerSlotDetails.Available_Slots;
                dealerSlot.Booked_Slots = dealerSlotDetails.Booked_Slots;
                dealerSlot.Total_Slots = dealerSlotDetails.Total_Slots;
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
