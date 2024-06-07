using DAL.Models;
using DAL.Repository.Implement;
using DAL.Repository.Interface;
using DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.Implement
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private FUMiniHotelManagementContext context = new FUMiniHotelManagementContext();
        private GenericRepository<BookingDetail> _bookingDetailRepository;
        private GenericRepository<BookingReservation> _bookingReservationRepository;
        private GenericRepository<Customer> _customerRepository;
        private GenericRepository<RoomInformation> _roomInformationRepository;
        private GenericRepository<RoomType> _roomTypeRepository;
        public UnitOfWork(FUMiniHotelManagementContext context)
        {
            this.context = context;
        }

        private bool disposed = false;
        public IGenericRepository<BookingDetail> BookingDetailRepository => _bookingDetailRepository ?? new GenericRepository<BookingDetail>(context);

        public IGenericRepository<BookingReservation> BookingResercationRepository => _bookingReservationRepository ?? new GenericRepository<BookingReservation>(context);

        public IGenericRepository<Customer> CustomerRepository => _customerRepository ?? new GenericRepository<Customer>(context);

        public IGenericRepository<RoomInformation> RoomInformationRepository => _roomInformationRepository ?? new GenericRepository<RoomInformation>(context);

        public IGenericRepository<RoomType> RoomTypeRepository => _roomTypeRepository ?? new GenericRepository<RoomType>(context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
