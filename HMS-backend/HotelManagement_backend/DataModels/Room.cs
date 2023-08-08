using System.ComponentModel.DataAnnotations;

namespace HotelManagement_backend.DataModels
{
    public class Room
    {
        [Key]
        public int RoomNumber { get; set; }
        public string Type { get; set; }
        public string AvailableStatus { get; set; }
        public int Price { get; set; }

        //Navigation Property
        public ICollection<Reservation> Reservations { get; set; }
    }
}
