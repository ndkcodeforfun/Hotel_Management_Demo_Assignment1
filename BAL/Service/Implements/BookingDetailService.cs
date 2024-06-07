using AutoMapper;
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
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<BookingDetail> GetAll()
        {
            try
            {
                var bookingDetail = _unitOfWork.BookingDetailRepository.Get().ToList();

                return bookingDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
