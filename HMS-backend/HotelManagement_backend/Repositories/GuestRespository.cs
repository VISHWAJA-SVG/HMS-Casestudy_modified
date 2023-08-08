using HotelManagement_backend.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement_backend.Repositories
{
    public class GuestRespository : IGuestRepository
    {
        private readonly HotelDbContext context;
        public GuestRespository(HotelDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Guest>> GetGuestsAsync()
        {
            return await context.Guests.ToListAsync();
        }
        public async Task<Guest> GetGuestAsync(Guid guestId)
        {
            return await context.Guests.FirstOrDefaultAsync(x => x.GuestId == guestId);
        }

        public Task<bool> Exists(Guid guestId)
        {
           return context.Guests.AnyAsync(x  => x.GuestId == guestId);
        }

        public async Task<Guest> UpdateGuestAsync(Guid guestId, Guest request)
        {
            var existingGuest = await GetGuestAsync(guestId);
            if(existingGuest != null) 
            {
                existingGuest.Name = request.Name;
                existingGuest.Email = request.Email;
                existingGuest.PhoneNumber= request.PhoneNumber;
                existingGuest.Age = request.Age;
                existingGuest.Gender = request.Gender;
                existingGuest.Address = request.Address;

                await context.SaveChangesAsync();
                return existingGuest;
            }
            return null;
        }

        public async Task<Guest> DeleteGuestAsync(Guid guestId)
        {
            var guest = await GetGuestAsync(guestId);
            if(guest != null) 
            {
                context.Guests.Remove(guest);
                await context.SaveChangesAsync();
                return guest;
            }
            return null;
        }

        public async Task<Guest> AddGuest(Guest request)
        {
            var guest = await context.Guests.AddAsync(request);
            await context.SaveChangesAsync();
            return guest.Entity;
        }
    }
}
