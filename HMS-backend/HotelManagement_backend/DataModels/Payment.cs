using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement_backend.DataModels
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public int Amount { get; set; }
        public string CreditCardDetails { get; set; }
        public DateTime PaymentTime { get; set; }
        //Navigation Property
        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public Bill bill { get; set; }
    }
}
