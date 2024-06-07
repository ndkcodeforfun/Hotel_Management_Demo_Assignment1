using DAL.Models;
using DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {

        IGenericRepository<BookingDetail> BookingDetailRepository { get; }
        IGenericRepository<BookingReservation> BookingResercationRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<RoomInformation> RoomInformationRepository { get; }
        IGenericRepository<RoomType> RoomTypeRepository { get; }
        void Save();
    }
}
