using BAL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var roomTypes = _roomTypeService.GetAll();
            if(roomTypes == null)
            {
                return NotFound("Room Type list not found");
            }
            return Ok(roomTypes);
        }
    }
}
