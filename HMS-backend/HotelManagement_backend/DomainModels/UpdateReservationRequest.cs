namespace HotelManagement_backend.DomainModels
{
    public class UpdateReservationRequest
    {
        public Guid ReservationId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfNights { get; set; }
        public int RoomNumber { get; set; }
    }
}
