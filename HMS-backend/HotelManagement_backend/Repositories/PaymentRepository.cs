using HotelManagement_backend.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace HotelManagement_backend.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly HotelDbContext context;
        public PaymentRepository(HotelDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Payment>> GetPaymentsAsync()
        {
            return await context.Payment.ToListAsync();
        }

        public async Task<Payment> GetPaymentAsync(Guid paymentId)
        {
            return await context.Payment.FirstOrDefaultAsync(x => x.PaymentId == paymentId);
        }


        public async Task<Payment> AddPaymentAsync(Payment request)
        {
            //request.PaymentTime = DateTime.UtcNow; // Set the payment time to the current time

            var payment = await context.Payment.AddAsync(request);
            await context.SaveChangesAsync();
            return payment.Entity;

            //request.PaymentTime = DateTime.UtcNow; // Set the payment time to the current time

            //// Retrieve the corresponding reservation
            //var reservation = await context.Reservations.FindAsync(request.ReservationId);

            //// Retrieve the corresponding room
            //var room = await context.Rooms.FindAsync(reservation.RoomNumber);

            //// Calculate the amount based on NumberOfNights and Price
            //request.Amount = reservation.NumberOfNights * room.Price;

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve // Preserve object references to handle cycles
            //};

            //options.IgnoreReadOnlyFields = true;
            //options.IgnoreReadOnlyProperties = true;

            //var payment = await context.Payment.AddAsync(request);
            //await context.SaveChangesAsync();
            //return payment.Entity;

            // Serialize the payment entity to remove the object cycle issue
            //var serializedPayment = JsonSerializer.Serialize(payment.Entity, options);
            //return JsonSerializer.Deserialize<Payment>(serializedPayment, options);
        }

    }
}
