using AutoMapper;

using HotelManagement_backend.DomainModels;
using HotelManagement_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HotelManagement_backend.Controllers
{
   // [Authorize(Roles = "Receptionist,Owner")]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper mapper;

        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllPaymentsAsync()
        {
            var payments = await paymentRepository.GetPaymentsAsync();

            return Ok(mapper.Map<List<Payment>>(payments));
        }

        [HttpGet]
        [Route("[controller]/{PaymentId:Guid}"), ActionName("GetPaymentAsync")]
        public async Task<IActionResult> GetPaymentAsync([FromRoute] Guid paymentId)
        {
            var payment = await paymentRepository.GetPaymentAsync(paymentId);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Payment>(payment));
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddPaymentAsync([FromBody] AddPaymentRequest request)
        {
            var payment = await paymentRepository.AddPaymentAsync(mapper.Map<DataModels.Payment>(request));
            return CreatedAtAction(nameof(GetPaymentAsync), new { paymentId = payment.PaymentId },
                mapper.Map<Payment>(payment));
        }

        [HttpGet]
        [Route("Bookings-per-day")]
        public async Task<IActionResult> GetReports()
        {
            var bookings = await paymentRepository.GetPaymentsAsync();
            var result = bookings.GroupBy(p => p.PaymentTime.Date)
            .Select(g => new
            {
           TotalAmount = g.Sum(p => p.Amount),
           TransactionCount = g.Count(),
           PaymentDate = g.Key
            }).ToList();

            return Ok(result);
        }
    }
}
