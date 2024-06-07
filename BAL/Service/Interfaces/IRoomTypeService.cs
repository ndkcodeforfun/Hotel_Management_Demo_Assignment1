using BAL.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service.Interfaces
{
    public interface IRoomTypeService
    {
        List<RoomTypeView> GetAll();
    }
}
