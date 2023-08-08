using HotelManagement_backend.DataModels;

namespace HotelManagement_backend.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetPaymentsAsync();
        Task<Payment> GetPaymentAsync(Guid paymentId);
        Task<Payment> AddPaymentAsync(Payment request);
    }
}
