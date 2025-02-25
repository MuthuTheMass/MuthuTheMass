// using AutoMapper;
// using CarParkingSystem.Application.Dtos.Dealers;
// using CarParkingSystem.Domain.Entities.SqlDatabase.DBModel;
// using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
// using CarParkingSystem.Infrastructure.Database.SQLDatabase.DBModel;
// using CarParkingSystem.Infrastructure.Database.SQLDatabase.SqlHelper;
// using Microsoft.EntityFrameworkCore;
//
// namespace ValidateCarParkingDetails.ValidateAuthorization
// {
//
//     public interface IDealerData
//     {
//         Task<List<DealerDto>> SearchData(Filter filter);
//
//         Task<bool?> UpsertDealerData(DealerDto dealerDto);
//
//         Task<bool> RemoveDealer(DeleteDealer delete);
//         
//         Task<DealerDto> SingleDealerDetails(string email);
//     }
//
//     public class DealerData : IDealerData
//     {
//         private readonly CarParkingBookingDbContext dbContext;
//         private readonly IMapper mapper;
//
//         public DealerData(CarParkingBookingDbContext _dbContext, IMapper _mapper)
//         {
//             mapper = _mapper;
//             dbContext = _dbContext;
//         }
//
//         public async Task<bool?> UpsertDealerData(DealerDto dealerDto)
//         {
//             var checkDuplicate = dbContext.DealerDetails.FirstOrDefault(g => g.DealerName == dealerDto.DealerName &&
//                                                                              g.DealerEmail == dealerDto.DealerEmail);
//
//             if (checkDuplicate is not null)
//             {
//                 mapper.Map(dealerDto, checkDuplicate);
//                 dbContext.DealerDetails.Update(checkDuplicate);
//                 //dbContext.Entry(checkDuplicate).State = EntityState.Modified;
//                 dbContext.SaveChanges();
//                 return true;
//             }
//             else
//             {
//                 return null;
//             }
//
//         }
//
//         //public Task<List<DealerVM>> SearchData(Filter filter)
//         //{
//         //    List<DealerDetails>? data;
//         //    var queryString = "SELECT * FROM dealerDetails ";
//
//
//         //    if (filter.filters.Any())
//         //    {
//         //        if (filter.filters.Any(b=>b.key.Contains("timing")))
//         //        {
//         //            queryString += " CROSS APPLY STRING_SPLIT(DealerTiming, '-') AS TimingSplit ";
//         //        }
//
//         //        foreach (var search in filter.filters)
//         //        {
//         //            if (search.key.ToLower().Contains("address"))
//         //            {
//         //                queryString = SqlHelper.clause(queryString, $" LOWER(DealerAddress) LIKE '%{search.value.ToLower()}%'");
//         //            }
//         //            if (search.key.ToLower().Contains("timing"))
//         //            {
//         //                if (search.key.ToLower().Contains("timingstart"))
//         //                {
//         //                    queryString = SqlHelper.clause(queryString, SqlHelper.jsonValueTiming("Start", search.value));
//         //                }
//         //                if (search.key.ToLower().Contains("timingstop"))
//         //                {
//         //                    queryString = SqlHelper.clause(queryString, SqlHelper.jsonValueTiming("Stop",search.value));
//         //                }
//         //            }
//         //        }
//         //    }
//
//         //    var query = dbContext.DealerDetails.FromSqlRaw(queryString);
//         //    data = query.ToList();
//
//
//         //    var result = mapper.Map<List<DealerVM>>(data);
//
//         //    return Task.FromResult(result);
//         //}
//
//         public Task<List<DealerDto>> SearchData(Filter filter)
//         {
//             List<DealerDetails>? data;
//             var queryString = "SELECT * FROM dealerDetails ";
//
//
//             foreach (var search in filter.filters)
//             {
//                 if (search.key.ToLower().Contains("address"))
//                 {    
//                     queryString = SqlHelper.clause(queryString, $" LOWER(DealerAddress) LIKE '%{search.value.ToLower()}%'");
//                 }
//             }
//
//             var query = dbContext.DealerDetails.FromSqlRaw(queryString);
//             data = query.Where(n=>n.DealerAddress !=null || 
//                                   n.DealerLandmark!=null ||
//                                   n.DealerGPSLocation != null ||
//                                   n.DealerRating != null).ToList();
//
//
//             var result = mapper.Map<List<DealerDto>>(data);
//
//             return Task.FromResult(result);
//         }
//
//
//
//         public Task<bool> RemoveDealer(DeleteDealer delete)
//         {
//             var isData = dbContext.DealerDetails.Where(d => d.DealerName == delete.DealerName ||
//                                                             d.DealerEmail == delete.DealerEmail ||
//                                                             d.DealerPhoneNo == delete.DealerPhoneNo).ToList();
//             if (isData.Count > 0)
//             {
//                 dbContext.DealerDetails.RemoveRange(isData);
//                 dbContext.SaveChanges();
//                 return Task.FromResult(true);
//             }
//             else
//             {
//                 return Task.FromResult(false);
//             }
//
//
//         }
//
//         public async Task<DealerDto> SingleDealerDetails(string email)
//         {
//             var gatherData = await dbContext.DealerDetails.Where(d => d.DealerEmail == email).FirstOrDefaultAsync();
//             return mapper.Map<DealerDto>(gatherData);
//         }
//         
//         
//         private string TimingSeperation(string date, int count)
//         {
//             var t = date.Substring(0, date.IndexOf("-"));
//             switch (count)
//             {
//                 case 1:
//                     return date.Split("-").First();
//                 case 2:
//                     return date.Split("-").Last();
//
//             }
//             return string.Empty;
//         }
//     }
// }
