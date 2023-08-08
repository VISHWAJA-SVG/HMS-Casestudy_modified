namespace HotelManagement_backend.DomainModels
{
    public class AddPaymentRequest
    {
        public Guid PaymentId { get; set; }
        public int Amount { get; set; }
        public string CreditCardDetails { get; set; }
        public DateTime PaymentTime { get; set; }
        public Guid ReservationId { get; set; }
    }
}
