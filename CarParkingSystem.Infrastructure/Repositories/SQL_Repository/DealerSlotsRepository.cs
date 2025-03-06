using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;

namespace CarParkingSystem.Infrastructure.Repositories.SQL_Repository
{
    public interface IDealerSlotsRepository
    {
        public DealerSlotDetails? GetSlotsByDealerId(string dealerId);

        public List<DealerSlotDetails> Get();
        
        public bool UpsertDealerSlots(DealerSlotDetails dealerSlotDetails);

        public bool DeleteDealerSlotsByDealerId(string dealerId);
    }

    public class DealerSlotsRepository : IDealerSlotsRepository
    {
        private readonly CarParkingBookingDbContext _dbContext;

        public DealerSlotsRepository(CarParkingBookingDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public bool DeleteDealerSlotsByDealerId(string dealerId)
        {
            if(string.IsNullOrEmpty(dealerId))
                return false;

            var dealerSlotDetails = _dbContext.DealerSlotDetails.FirstOrDefault(d => d.DealerId == dealerId);
            if (dealerSlotDetails == null)
                return false;

            _dbContext.DealerSlotDetails.Remove(dealerSlotDetails);
            _dbContext.SaveChanges();
            return true;

        }

        public List<DealerSlotDetails> Get()
        {
            List<DealerSlotDetails> dealerSlotDetails = _dbContext.DealerSlotDetails.ToList();
            if(dealerSlotDetails == null || dealerSlotDetails.Count <=0)
                return new List<DealerSlotDetails>();
            return dealerSlotDetails;
        }

        public DealerSlotDetails? GetSlotsByDealerId(string dealerId)
        {
            if(string.IsNullOrEmpty(dealerId))
                return null;

            DealerSlotDetails? dealerSlotDetails = _dbContext.DealerSlotDetails.FirstOrDefault(d => d.DealerId == dealerId);

            if(dealerSlotDetails == null)
                return new DealerSlotDetails() { Id ="none" };

            return dealerSlotDetails;

        }

        public bool UpsertDealerSlots(DealerSlotDetails dealerSlotDetails)
        {
            if(dealerSlotDetails == null)
                return false;

            var dealerSlot = _dbContext.DealerSlotDetails.FirstOrDefault(d => d.DealerId == dealerSlotDetails.DealerId);
            if (dealerSlot == null)
            {
                _dbContext.DealerSlotDetails.Add(dealerSlotDetails);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                dealerSlot.Available_Slots = dealerSlotDetails.Available_Slots;
                dealerSlot.Booked_Slots = dealerSlotDetails.Booked_Slots;
                dealerSlot.Total_Slots = dealerSlotDetails.Total_Slots;
                _dbContext.SaveChanges();
                return true;
            }
    }
}
