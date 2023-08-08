using AutoMapper;
using HotelManagement_backend.DomainModels;
using HotelManagement_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HotelManagement_backend.Controllers
{
    [ApiController]
    //[Authorize(Roles ="Receptionist")]
    public class GuestsController : Controller
    {
        private readonly IGuestRepository guestRepository;
        private readonly IMapper mapper;
        public GuestsController(IGuestRepository guestRepository,IMapper mapper)
        {
            this.guestRepository = guestRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGuestsAsync()
        {
            var guests = await guestRepository.GetGuestsAsync();
            return Ok(mapper.Map<List<Guest>>(guests));
        }

        [HttpGet]
        [Route("[controller]/{guestId:guid}"),ActionName("GetGuestAsync")]
        public async Task<IActionResult> GetGuestAsync([FromRoute] Guid guestId)
        {
            var guest= await guestRepository.GetGuestAsync(guestId);
            if(guest == null) 
            {
                return NotFound();
            }
            return Ok(mapper.Map<Guest>(guest));
        }
        [HttpPut]
        [Route("[controller]/{guestId:guid}")]
        public async Task<IActionResult> UpdateGuestAsync([FromRoute] Guid guestId,[FromBody] UpdateGuestRequest request)
        {
            if(await guestRepository.Exists(guestId))
            {
               var updatedguest = await guestRepository.UpdateGuestAsync(guestId, mapper.Map<DataModels.Guest>(request));
                if(updatedguest != null) 
                {
                    return Ok(mapper.Map<Guest>(updatedguest));
                }
            }
              return NotFound();
            
        }
        [HttpDelete]
        [Route("[controller]/{guestId:guid}")]
        public async Task<IActionResult> DeleteGuestAsync([FromRoute] Guid guestId)
        {
            if(await guestRepository.Exists(guestId))
            {
                var guest = await guestRepository.DeleteGuestAsync(guestId);
                return Ok(mapper.Map<Guest>(guest));
            }
            return NotFound();
        }
        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddGuestAsync([FromBody] AddGuestRequest request)
        {
            var guest = await guestRepository.AddGuest(mapper.Map<DataModels.Guest>(request));
            return CreatedAtAction(nameof(GetGuestAsync),new {guestId = guest.GuestId},
                mapper.Map<Guest>(guest));
        }
    }
}
