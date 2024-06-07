using BAL.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service.Interfaces
{
    public interface IRoomInformationService
    {
        List<RoomInformationView> GetAllActiveRoom();

        List<RoomInformationView> GetAllRoom();

        RoomInformationView GetRoomById(int id);

        bool isCreatedRoom(RoomInformationView roomInformationView);

        bool isUpdatedRoom(int id, RoomInformationView roomInformationView);

        bool isSetStatusRoom(int id);
    }
}
