using AutoMapper;
using HotelManagement_backend.DomainModels;
using HotelManagement_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement_backend.Controllers
{
    [ApiController]
   // [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper mapper;
        public RoomController(IRoomRepository roomRepository, IMapper mapper)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        //[Authorize(Roles = "Receptionist,Manager")]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            var rooms = await roomRepository.GetRoomsAsync();
            return Ok(mapper.Map<List<Room>>(rooms));
        }

        [HttpGet]
        [Route("[controller]/{Roomnumber:int}"), ActionName("GetRoomAsync")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetRoomAsync([FromRoute] int Roomnumber)
        {
            var room = await roomRepository.GetRoomAsync(Roomnumber);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Room>(room));
        }

        [HttpPut]
        [Route("[controller]/{Roomnumber:int}")]
       // [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateRoomAsync([FromRoute] int Roomnumber, [FromBody] UpdateRoomRequest request)
        {
            if (await roomRepository.Exists(Roomnumber))
            {
                var updatedRoom = await roomRepository.UpdateRoom(Roomnumber, mapper.Map<DataModels.Room>(request));
                if (updatedRoom != null)
                {
                    return Ok(mapper.Map<Room>(updatedRoom));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{Roomnumber:int}")]
       // [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteRoomAsync([FromRoute] int Roomnumber)
        {
            if (await roomRepository.Exists(Roomnumber))
            {
                var room = await roomRepository.DeleteRoom(Roomnumber);
                return Ok(mapper.Map<Room>(room));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        //[Authorize(Roles ="Manager")]
        public async Task<IActionResult> AddRoomAsync([FromBody] AddRoomRequest request)
        {
            var room = await roomRepository.AddRoom(mapper.Map<DataModels.Room>(request));
            return CreatedAtAction(nameof(GetRoomAsync), new { Roomnumber = room.RoomNumber },
                mapper.Map<Room>(room));
        }

        [HttpGet("{status}")]
        //  [Route("[controller]/{status:string}")]
       // [Authorize(Roles = "Receptionist,Manager")]
        public async Task<IActionResult> SearchRooms([FromRoute] string status)
        {
            var rooms = await roomRepository.SearchRoom(status);
            if (rooms == null || rooms.Count == 0)
            {
                return NotFound();
            }

            var mappedRooms = mapper.Map<List<Room>>(rooms);

            return Ok(mappedRooms);
        }
    }
}
