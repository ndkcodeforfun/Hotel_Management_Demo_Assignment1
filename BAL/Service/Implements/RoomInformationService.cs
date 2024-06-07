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
    public class RoomInformationService : IRoomInformationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomInformationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<RoomInformationView> GetAllActiveRoom()
        {
            try
            {
                var rooms = _unitOfWork.RoomInformationRepository.Get().Where(r => r.RoomStatus == 1).ToList();
                if (rooms.Count == 0)
                {
                    return null;
                }
                List<RoomInformationView> roomViews = new List<RoomInformationView>();
                foreach (var room in rooms)
                {
                    var roomView = _mapper.Map<RoomInformationView>(room);
                    roomViews.Add(roomView);
                }
                return roomViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RoomInformationView> GetAllRoom()
        {
            try
            {
                var rooms = _unitOfWork.RoomInformationRepository.Get().ToList();
                if (rooms.Count == 0)
                {
                    return null;
                }
                List<RoomInformationView> roomViews = new List<RoomInformationView>();
                foreach (var room in rooms)
                {
                    var roomView = _mapper.Map<RoomInformationView>(room);
                    roomViews.Add(roomView);
                }
                return roomViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RoomInformationView GetRoomById(int id)
        {
            try
            {
                var room = _unitOfWork.RoomInformationRepository.GetByID(id);
                if (room == null)
                {
                    return null;
                }
                var roomView = _mapper.Map<RoomInformationView>(room);
                return roomView;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool isCreatedRoom(RoomInformationView roomInformationView)
        {
            try
            {
                bool status = false;
                roomInformationView.RoomStatus = 1;
                var room = _mapper.Map<RoomInformation>(roomInformationView);
                _unitOfWork.RoomInformationRepository.Insert(room);
                _unitOfWork.Save();
                status = true;
                return status;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool isSetStatusRoom(int id)
        {
            try
            {
                bool status = false;
                var roomExisted = _unitOfWork.RoomInformationRepository.GetByID(id);
                if (roomExisted != null) {
                    if(roomExisted.RoomStatus == 1)
                    {
                        roomExisted.RoomStatus = 2;
                        _unitOfWork.RoomInformationRepository.Update(roomExisted);
                        _unitOfWork.Save();
                        status = true;
                        return status;
                    }else if(roomExisted.RoomStatus == 2)
                    {
                        roomExisted.RoomStatus = 1;
                        _unitOfWork.RoomInformationRepository.Update(roomExisted);
                        _unitOfWork.Save();
                        status = true;
                        return status;
                    }
                    
                }
                return status;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public bool isUpdatedRoom(int id, RoomInformationView roomInformationView)
        {
            try
            {
                var checkExist = _unitOfWork.RoomInformationRepository.GetByID(id);
                if(checkExist != null)
                {
                    bool status = false;
                    roomInformationView.RoomStatus = 1;
                    _mapper.Map(roomInformationView, checkExist);
                    _unitOfWork.RoomInformationRepository.Update(checkExist);
                    _unitOfWork.Save();
                    status = true;
                    return status;
                }
                return false;
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
