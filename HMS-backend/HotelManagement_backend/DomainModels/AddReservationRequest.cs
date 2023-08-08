namespace HotelManagement_backend.DomainModels
{
    public class AddReservationRequest
    {
        public Guid ReservationId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfNights { get; set; }
        public Guid GuestId { get; set; }
        public int RoomNumber { get; set; }
    }
}
