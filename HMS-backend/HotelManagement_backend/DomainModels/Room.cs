using HotelManagement_backend.DataModels;

namespace HotelManagement_backend.DomainModels
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public string Type { get; set; }
        public string AvailableStatus { get; set; }
        public int Price { get; set; }

        //Navigation Property
        public ICollection<Reservation> Reservations { get; set; }
    }
}
