namespace HotelManagement_backend.DomainModels
{
    public class LoginResponseDTO
    {
        public string JwtToken { get; set; }
        public string Role { get; set; }
    }
}
