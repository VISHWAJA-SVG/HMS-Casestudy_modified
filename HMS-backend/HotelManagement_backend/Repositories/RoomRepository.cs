using HotelManagement_backend.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement_backend.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public readonly HotelDbContext context;
        public RoomRepository(HotelDbContext context)
        {
            this.context = context;
        }

     
        public async Task<List<Room>> GetRoomsAsync()
        {
           // return context.Rooms.Include(nameof(Reservation)).ToList();
            return await context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomAsync(int Roomnumber)
        {
            return await context.Rooms.FirstOrDefaultAsync(x => x.RoomNumber == Roomnumber);
        }

        public async Task<bool> Exists(int Roomnumber)
        {
            return await context.Rooms.AnyAsync(x=>x.RoomNumber == Roomnumber);
        }

        public async Task<Room> UpdateRoom(int Roomnumber, Room request)
        {
            var existingRoom=await GetRoomAsync(Roomnumber);
            if (existingRoom != null) 
            {
                existingRoom.Type=request.Type;
                existingRoom.AvailableStatus=request.AvailableStatus;
                existingRoom.Price=request.Price;

                await context.SaveChangesAsync();

                return existingRoom;
            }
            return null;
        }

        public async Task<Room> DeleteRoom(int Roomnumber)
        {
            var room = await GetRoomAsync(Roomnumber);
            if(room != null)
            {
                context.Rooms.Remove(room);
                await context.SaveChangesAsync();
                return room;
            }
            return null;
        }

        public async Task<Room> AddRoom(Room request)
        {
            var room= await context.Rooms.AddAsync(request);
            await context.SaveChangesAsync();
            return room.Entity;
        }

        public async Task<List<Room>> SearchRoom(string status)
        {
            var roomsQuery = context.Rooms.AsQueryable();


            if (!string.IsNullOrEmpty(status))
            {
                roomsQuery = roomsQuery.Where(room => room.AvailableStatus == status);
            }

            var rooms = await roomsQuery.ToListAsync();
            return rooms;
        }

        
  
    }
}
