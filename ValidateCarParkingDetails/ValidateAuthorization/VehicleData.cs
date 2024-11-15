using AutoMapper;
using CarParkingBookingDatabase.BookingDBContext;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingDatabase.Migrations;
using CarParkingBookingVM.VM_S.Vehicle;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidateCarParkingDetails.ValidateAuthorization
{
    public interface IVehicleData
    {
        Task<bool?> UpsertVehicle(string userId,VehicleVM vehicle);
        Task<List<Vehicle_User_VM>?> GetVehicleDetailsBy_UserID(string userID,bool halfDetials);
        Task<Vehicle_User_VM?> GetVehicleDetailsSingle(string userID,string vehicleId);
        Task<Vehicle_User_VM?> GetDetailsByVehicleNumber(string userId, string vehicleNumber);
        Task<bool?> RemoveVehicle(string userId,string vehicleId);
    }

    public class VehicleData : IVehicleData
    {
        private readonly CarParkingBookingDBContext dbContext;
        private readonly IMapper mapper;
        public VehicleData(CarParkingBookingDBContext _dbContext,IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        public async Task<Vehicle_User_VM?> GetDetailsByVehicleNumber(string userId,string vehicleNumber)
        {
            var vehicleData = await dbContext.VehicleDetails.SingleOrDefaultAsync(g=> 
                                                                                  g.VehicleNumber.Equals(vehicleNumber) &&
                                                                                  g.UserID.Equals(userId));
            if(vehicleData != null)
            {
                return mapper.Map<Vehicle_User_VM>(vehicleData);
            }
            else
            {
                return null;
            }

        }

        public async Task<List<Vehicle_User_VM>?> GetVehicleDetailsBy_UserID(string userID,bool halfDetials)
        {
            List<VehicleDetails>? vehicleData = await dbContext.VehicleDetails.Where(x => x.UserID == userID).ToListAsync();

            if (vehicleData.Count > 0) 
            {
                var data = mapper.Map<List<Vehicle_User_VM>>(vehicleData);
                if (halfDetials)
                { 
                    var shortData = new List<Vehicle_User_VM>();
                    foreach (var vehicleDetails in vehicleData)
                    {
                        shortData.Add(new Vehicle_User_VM()
                        {
                            VehicleNumber = vehicleDetails.VehicleNumber,
                            VehicleName = vehicleDetails.VehicleName,
                        });
                    }
                    
                    return shortData;
                }
                return data;
            }
            else
            {
                return null;
            }
        }

        public async Task<Vehicle_User_VM?> GetVehicleDetailsSingle(string userID, string vehicleId)
        {
            var vehicleData = dbContext.VehicleDetails.Where(v => v.UserID == userID && v.VehicleId == vehicleId).ToList();

            if(vehicleData is not null)
            {
                var data = mapper.Map<Vehicle_User_VM>(vehicleData.FirstOrDefault());
                return data;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool?> RemoveVehicle(string userId, string vehicleId)
        {
            var vehicleData = dbContext.VehicleDetails.Where(v => v.UserID == userId && v.VehicleId == vehicleId).ToList();

            if (vehicleData is not null)
            {
                dbContext.VehicleDetails.RemoveRange(vehicleData);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool?> UpsertVehicle(string userId, VehicleVM vehicle)
        {
            var vehicleData = dbContext.VehicleDetails.Where(v => v.UserID == userId && v.VehicleNumber == vehicle.VehicleNumber).FirstOrDefault();

            if(vehicleData is not null)
            {
                mapper.Map(vehicle,vehicleData);
                vehicleData.UserID = userId;
                dbContext.VehicleDetails.Update(vehicleData);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                var data = mapper.Map<VehicleDetails>(vehicle);
                data.UserID = userId;
                await dbContext.VehicleDetails.AddAsync(data);
                await dbContext.SaveChangesAsync();
                return false;
            }
            return null;
        }
    }
}
