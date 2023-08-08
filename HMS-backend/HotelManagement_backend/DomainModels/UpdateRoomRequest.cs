namespace HotelManagement_backend.DomainModels
{
    public class UpdateRoomRequest
    {
        public string Type { get; set; }
        public string AvailableStatus { get; set; }
        public int Price { get; set; }
    }
}
