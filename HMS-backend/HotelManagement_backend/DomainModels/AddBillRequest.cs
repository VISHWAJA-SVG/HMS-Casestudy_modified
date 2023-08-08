namespace HotelManagement_backend.DomainModels
{
    public class AddBillRequest
    {
        public Guid BillId { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid PaymentId { get; set; }
    }
}
