using HotelManagement_backend.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement_backend.Repositories
{
    public class ReservationRepository:IReservationRepository
    {
        public readonly HotelDbContext context;

        public ReservationRepository(HotelDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
           // return await context.Reservations.Include(nameof(Room)).ToListAsync();
            return await context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservationAsync(Guid reservationId)
        {
            return await context.Reservations.FirstOrDefaultAsync(x => x.ReservationId == reservationId);
        }

        public async Task<Reservation> CreateReservationAsync(Reservation request)
        {
            var reservation = await context.Reservations.AddAsync(request);
            await context.SaveChangesAsync();
            return reservation.Entity;
        }

        public Task<bool> Exists(Guid reservationId)
        {
            return context.Reservations.AnyAsync(x => x.ReservationId == reservationId);
        }

        public async Task<Reservation> UpdateReservationAsync(Guid reservationId, Reservation request)
        {
            var existingBooking = await GetReservationAsync(reservationId);
            if (existingBooking != null)
            {
                existingBooking.NumberOfAdults = request.NumberOfAdults;
                existingBooking.NumberOfChildren = request.NumberOfChildren;
                existingBooking.CheckInDate = request.CheckInDate;
                existingBooking.CheckOutDate = request.CheckOutDate;
                existingBooking.NumberOfNights = request.NumberOfNights;
                existingBooking.RoomNumber = request.RoomNumber;

                await context.SaveChangesAsync();
                return existingBooking;
            }
            return null;
        }

        public async Task<Reservation> DeleteReservationAsync(Guid reservationId)
        {
            var booking = await GetReservationAsync(reservationId);
            if (booking != null)
            {
                context.Reservations.Remove(booking);
                await context.SaveChangesAsync();
                return booking;
            }
            return null;
        }
    }
}
