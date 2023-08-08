using AutoMapper;
using HotelManagement_backend.DomainModels;
using HotelManagement_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HotelManagement_backend.Controllers
{
    //[Authorize(Roles = "Receptionist")]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;

        public ReservationController(IReservationRepository reservationRepository,IMapper mapper)
        {
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllResevationsAsync()
        {
            var reservations = await reservationRepository.GetReservationsAsync();


            return Ok(mapper.Map<List<Reservation>>(reservations));
        }

        [HttpGet]
        [Route("[controller]/{reservationId:Guid}"), ActionName("GetReservationAsync")]
        public async Task<IActionResult> GetReservationAsync([FromRoute] Guid reservationId)
        {
            var reservation = await reservationRepository.GetReservationAsync(reservationId);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Reservation>(reservation));
        }

        [HttpPut]
        [Route("[controller]/{reservationId:Guid}")]
        public async Task<IActionResult> UpdateReservationAsync([FromRoute] Guid reservationId, [FromBody] UpdateReservationRequest request)
        {
            if (await reservationRepository.Exists(reservationId))
            {
                var updatedBooking = await reservationRepository.UpdateReservationAsync(reservationId, mapper.Map<DataModels.Reservation>(request));
                if (updatedBooking != null)
                {
                    return Ok(mapper.Map<Reservation>(updatedBooking));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{reservationId:Guid}")]
        public async Task<IActionResult> DeleteReservationAsync([FromRoute] Guid reservationId)
        {
            if (await reservationRepository.Exists(reservationId))
            {
                var booking = await reservationRepository.DeleteReservationAsync(reservationId);
                return Ok(mapper.Map<Reservation>(booking));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> CreateReservationAsync([FromBody] AddReservationRequest request)
        {
            var reservation = await reservationRepository.CreateReservationAsync(mapper.Map<DataModels.Reservation>(request));
            return CreatedAtAction(nameof(GetReservationAsync), new { reservationId = reservation.ReservationId },
                mapper.Map<Reservation>(reservation));
        }
    }
}
