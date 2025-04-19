using AutoMapper;
using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;

namespace CarParkingAPI.AutoMapper.Resolver
{
    public class CarParkingBookingDto
    {
    }

    public class VehicleInfoResolver : IValueResolver<BookingDto, CarBooking, VehicleInformation>
    {
        public VehicleInformation Resolve(BookingDto source, CarBooking destination, VehicleInformation destMember, ResolutionContext context)
        {
            return source.VehicleInfo ?? new VehicleInformation();
        }
    }

    public class CustomerDataResolver : IValueResolver<BookingDto, CarBooking, CustomerUserDetails>
    {
        public CustomerUserDetails Resolve(BookingDto source, CarBooking destination, CustomerUserDetails destMember, ResolutionContext context)
        {
            var proof = source.customerDetails.Proof;

            return new CustomerUserDetails
            {
                CustomerId = source.CustomerId ?? string.Empty,
                CustomerName = source.customerDetails.CustomerName,
                CustomerEmail = source.customerDetails.Email ?? string.Empty,
                CustomerMobileNumber = source.customerDetails.MobileNumber,
                CustomerAddress = source.customerDetails.Address,
                CustomerProof = proof?.Type,
                CustomerProofNumber = proof?.Number,
                CustomerAuthorityOfIssue = null // Not available in source
            };
        }
    }

    public class PaymentResolver : IValueResolver<BookingDto, CarBooking, PaymentInfo>
    {
        public PaymentInfo Resolve(BookingDto source, CarBooking destination, PaymentInfo destMember, ResolutionContext context)
        {
            return new PaymentInfo
            {
                AdvanceAmount = source.AdvanceAmount,
                Source = source.BookingSource
            };
        }
    }


}
