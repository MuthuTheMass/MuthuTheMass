using AutoMapper;
using CarParkingBookingDatabase.BookingDBContext;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.VM_S.Dealers;
using Microsoft.EntityFrameworkCore;

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
                dbContext.dealerDetails.Add(data);
                dbContext.SaveChanges();
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
            var query = dbContext.dealerDetails;


            if (filter.filters.Any())
            {
                foreach (var search in filter.filters)
                {
                    if (search.key.ToLower().Contains("address"))
                    {
                        query = (DbSet<DealerDetails>)query.FromSqlRaw($"SELECT * FROM dealerDetails WHERE LOWER(DealerAddress) LIKE '%{search.value}%'");
                    }
                    if (search.key.ToLower().Contains("timing"))
                    {
                        if (search.key.ToLower().Contains("timingstart"))
                        {
                            query = (DbSet<DealerDetails>)query.FromSqlRaw($"SELECT * FROM dealerDetails CROSS APPLY STRING_SPLIT(DealerTiming, '-') AS TimingSplit WHERE TRIM(TimingSplit.value) = '{search.value}';");
                        }
                        if (search.key.ToLower().Contains("timingstop"))
                        {
                            query = (DbSet<DealerDetails>)query.FromSqlRaw($"SELECT * FROM dealerDetails CROSS APPLY STRING_SPLIT(DealerTiming, '-') AS TimingSplit WHERE TRIM(TimingSplit.value) = '{search.value}';");
                        }
                    }
                }

                
            }

            data = query.ToList();

            
            var result = mapper.Map<List<DealerVM>>(data);

            return Task.FromResult(result);
        }


        private string TimingSeperation(string date,int count)
        {
            var t = date.Substring(0, date.IndexOf("-"));
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
