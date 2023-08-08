namespace HotelManagement_backend.DataModels
{
    public class Guest
    {
        public Guid GuestId {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set;}
        public int Age { get; set;}
        public string Address { get; set; }
        //Navigation Property
        public ICollection<Reservation> Reservations { get; set; }
    }
}
