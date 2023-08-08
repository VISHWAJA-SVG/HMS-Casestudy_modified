using HotelManagement_backend.DataModels;

namespace HotelManagement_backend.Repositories
{
    public interface IBillRepository
    {
        Task<Bill> GetBillAsync(Guid billId);
        Task<Bill> AddBillDetails(Bill request);
    }
}
