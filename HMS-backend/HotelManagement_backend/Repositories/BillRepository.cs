using HotelManagement_backend.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement_backend.Repositories
{
    public class BillRepository:IBillRepository
    {
        private readonly HotelDbContext context;
        public BillRepository(HotelDbContext context)
        {
            this.context = context;
        }

        public async Task<Bill> AddBillDetails(Bill request)
        {
            // Retrieve payment information based on PaymentId
            Payment payment = await context.Payment
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(p => p.PaymentId == request.PaymentId);

            if (payment == null || payment.Reservation == null)
            {
                // Handle the case where the payment or reservation is not found
                throw new Exception("Invalid payment or reservation.");
            }

            Reservation reservation = payment.Reservation;

            // Set CheckinDate and CheckoutDate from the reservation
            request.CheckinDate = reservation.CheckInDate;
            request.CheckoutDate = reservation.CheckOutDate;

            // Calculate TaxAmount based on the payment amount
            if (payment.Amount > 1000)
            {
                request.TaxAmount = payment.Amount * 0.25m;
            }
            else
            {
                request.TaxAmount = 0;
            }

            // Calculate TotalAmount as the sum of payment amount and tax amount
           // request.TotalAmount = payment.Amount + request.TaxAmount;
            request.TotalAmount = payment.Amount;

            var bill = await context.Bill.AddAsync(request);
            await context.SaveChangesAsync();

            bill.Entity.payment = null;
            //bill.Entity.payment.Reservation = null;
            return bill.Entity;
        }

        public async Task<Bill> GetBillAsync(Guid billId)
        {
            return await context.Bill.FirstOrDefaultAsync(x => x.BillId == billId);
        }

    }
}
