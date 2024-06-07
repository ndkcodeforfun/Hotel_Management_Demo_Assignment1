using AutoMapper;
using BAL.ModelView;
using BAL.Service.Interfaces;
using DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service.Implements
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<RoomTypeView> GetAll()
        {
            try
            {
                var roomTypes = _unitOfWork.RoomTypeRepository.Get().ToList();
                if (roomTypes.Count == 0)
                {
                    return null;
                }
                List<RoomTypeView> roomTypeViews = new List<RoomTypeView>();
                foreach (var roomType in roomTypes)
                {
                    var roomTypeView = _mapper.Map<RoomTypeView>(roomType);
                    roomTypeViews.Add(roomTypeView);
                }
                return roomTypeViews;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
