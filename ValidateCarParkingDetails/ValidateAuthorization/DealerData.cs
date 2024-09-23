using AutoMapper;
using CarParkingBookingDatabase.BookingDBContext;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.VM_S.Dealers;

namespace ValidateCarParkingDetails.ValidateAuthorization
{

    public interface IDealerData
    {
        Task<List<DealerVM>> SearchData(Filter filter);

        Task<bool> AddDealerData(DealerVM dealerVM);
    }

    public class DealerData : IDealerData
    {
        private readonly CarParkingBookingDBContext dbContext;
        private readonly IMapper mapper;

        public DealerData(CarParkingBookingDBContext _dbContext,IMapper _mapper)
        {
            mapper = _mapper;
            dbContext = _dbContext;
        }

        public async Task<bool> AddDealerData(DealerVM dealerVM)
        {
            if (!string.IsNullOrEmpty(dealerVM.DealerName))
            {
                var data = mapper.Map<DealerDetails>(dealerVM);
                await dbContext.dealerDetails.AddAsync(data);
                await dbContext.SaveChangesAsync();
                return true;

            }
            else
            {
                return false;
            }
        }

        public Task<List<DealerVM>> SearchData(Filter filter)
        {
            List<DealerDetails>? data;
            var query = dbContext.dealerDetails.AsQueryable();


            if (filter.filters.Any())
            {
                foreach (var search in filter.filters)
                {
                    if (search.key.ToLower().Contains("address"))
                    {
                        query = query.Where(n=>n.DealerAddress.Contains(search.value));
                    }
                    if (search.key.ToLower().Contains("timing"))
                    {
                        query = query.Where(n => TimingSeperation(n.DealerTiming,1) == search.value)
                                     .Where(y => TimingSeperation(y.DealerTiming,2) == search.value);
                    }
                }

                
            }

            data = query.ToList();
            var result = mapper.Map<List<DealerVM>>(data);

            return Task.FromResult(result);
        }


        private string TimingSeperation(string date,int count)
        {
            switch (count)
            {
                case 1:
                    return date.Split("-").First();
                case 2:
                    return date.Split("-").Last();

            }
            return string.Empty;
        }
    }
}
