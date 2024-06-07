using AutoMapper;
using BAL.ModelView;
using BAL.Service.Interfaces;
using DAL.Models;
using DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service.Implements
{
    public class BookingReservationService : IBookingReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void DeleteBookingReservation(BookingReservation bookingReservation)
        {
            throw new NotImplementedException();
        }

        public List<BookingReservation> GetAll()
        {
            try
            {
                var booking = _unitOfWork.BookingResercationRepository.Get().ToList();
                
                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookingReservation GetByBookingReservationId(int? id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookingReservation(BookingReservation bookingReservation)
        {
            throw new NotImplementedException();
        }
    }
}
