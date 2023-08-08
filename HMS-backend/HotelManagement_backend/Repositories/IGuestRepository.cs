using HotelManagement_backend.DataModels;

namespace HotelManagement_backend.Repositories
{
    public interface IGuestRepository
    {
        Task<List<Guest>> GetGuestsAsync();
        Task<Guest> GetGuestAsync(Guid guestId);
        Task<bool> Exists(Guid guestId);
        Task<Guest> UpdateGuestAsync(Guid guestId, Guest request);
        Task<Guest> DeleteGuestAsync(Guid guestId);
        Task<Guest> AddGuest(Guest request);
    }
}
