using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement_backend.DataModels
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfNights { get; set; }
       
        //Navigation Properties
        [ForeignKey("Guest")]
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; }
        [ForeignKey("Room")]
        public int RoomNumber { get; set; }
        public Room Room { get; set; }
        public Payment Payment { get; set; }
    }
}
