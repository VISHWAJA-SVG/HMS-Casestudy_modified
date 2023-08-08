using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement_backend.DomainModels
{
    public class Bill
    {
            public Guid BillId { get; set; }
            public DateTime CheckinDate { get; set; }
            public DateTime CheckoutDate { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal TotalAmount { get; set; }

            //Navigation properties
            [ForeignKey("payment")]
            public Guid PaymentId { get; set; }
            public Payment payment { get; set; }
    }
}
