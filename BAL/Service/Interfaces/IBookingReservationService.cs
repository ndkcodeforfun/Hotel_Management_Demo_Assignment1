using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service.Interfaces
{
    public interface IBookingReservationService
    {
        List<BookingReservation> GetAll();
        BookingReservation GetByBookingReservationId(int? id);
        void UpdateBookingReservation(BookingReservation bookingReservation);
        void DeleteBookingReservation(BookingReservation bookingReservation);
    }
}
