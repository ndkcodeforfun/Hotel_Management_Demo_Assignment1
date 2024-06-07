using BAL.ModelView;
using BAL.Service.Implements;
using BAL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomInformationController : ControllerBase
    {
        private readonly IRoomInformationService _roomInformationService;
        private readonly IRoomTypeService _roomTypeService;

        public RoomInformationController(IRoomInformationService roomInformationService, IRoomTypeService roomTypeService)
        {
            _roomInformationService = roomInformationService;
            _roomTypeService = roomTypeService;
        }
        [HttpGet]
        public IActionResult GetAllRoom()
        {
            var roomList = _roomInformationService.GetAllActiveRoom();
            if(roomList == null)
            {
                return NotFound("Room List not available");
            }
            return Ok(roomList);
        }

        [HttpGet("/api/v1/RoomInformation/{id}")]
        public IActionResult GetRoomById(int id)
        {
            var room = _roomInformationService.GetRoomById(id);
            if (room == null)
            {
                return NotFound("Customer not found");
            }
            return Ok(room);
        }

        [HttpPost("/api/v1/RoomInformation")]
        public IActionResult CreateRoom([FromBody] RoomInformationView roomInformationView)
        {
            if(roomInformationView == null)
            {
                return BadRequest();
            }
            var roomType = _roomTypeService.GetAll().Where(r => r.RoomTypeId == roomInformationView.RoomTypeId).FirstOrDefault();
            if(roomType == null)
            {
                return BadRequest("Room type does not exist");
            }

            bool checkInsert = _roomInformationService.isCreatedRoom(roomInformationView);
            if(checkInsert)
            {
                return Ok("Create successfully!");
            }
            else
            {
                return BadRequest("Insert faul!");
            }

        }

        [HttpPut("/api/v1/RoomInformation/{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] RoomInformationView roomInformationView)
        {
            if (roomInformationView == null)
            {
                return BadRequest();
            }
            var roomType = _roomTypeService.GetAll().Where(r => r.RoomTypeId == roomInformationView.RoomTypeId).FirstOrDefault();
            if (roomType == null)
            {
                return BadRequest("Room type does not exist");
            }

            bool checkUpdate = _roomInformationService.isUpdatedRoom(id, roomInformationView);
            if (checkUpdate)
            {
                return Ok("Update successfully!");
            }
            else
            {
                return BadRequest("Room is not available!");
            }

        }

        [HttpPut("/api/v1/RoomInformation/setStatus/{id}")]
        public IActionResult SetStatusRoom(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var checkDelete = _roomInformationService.isSetStatusRoom(id);
            if(checkDelete)
            {
                return Ok("Set status successfully!");
            }
            else
            {
                return BadRequest("Room is not available");
            }

        }
    }
}
