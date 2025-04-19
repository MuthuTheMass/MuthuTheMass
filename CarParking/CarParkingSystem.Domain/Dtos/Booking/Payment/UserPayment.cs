using CarParkingSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSystem.Domain.Dtos.Booking.Payment
{
    public class UserPayment
    {
        public string? PaymentId { get; set; }
        public string? BookingId { get; set; }
        public string? CustomerEmail { get; set; }
        public Currency? CurrencyMode { get; set; }
        public BookingStatus? PaymentStatus { get; set; }
        public modeOfPayment? PaymentMethod { get; set; }
        public string? Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
