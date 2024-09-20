using AutoMapper;
using CarParkingBookingDatabase.BookingDBContext;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.VM_S.Dealers;

namespace ValidateCarParkingDetails.ValidateAuthorization
{

    public interface IDealerData
    {
        Task<List<DealerVM>> SearchData(Filter filter);
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

        public Task<List<DealerVM>> SearchData(Filter filter)
        {
            List<DealerDetails>? data;
            var query = dbContext.dealerDetails.AsQueryable();


            if (filter.filters.Any())
            {
                foreach (var search in filter.filters)
                {
                    if (search.key.Contains("Address"))
                    {
                        query = query.Where(n=>n.DealerAddress.Contains(search.value));
                    }
                    if (search.key.Contains("Timing"))
                    {
                        query = query.Where(n => TimingSeperation(n.DealerTiming) == search.value);
                    }
                }

                
            }

            data = query.ToList();
            var result = mapper.Map<List<DealerVM>>(data);

            return Task.FromResult(result);
        }


        public string TimingSeperation(string date)
        {
            return date.Split("-").First();
        }
    }
}
