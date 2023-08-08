using System.ComponentModel.DataAnnotations;

namespace HotelManagement_backend.DomainModels
{
    public class RegisterRequestDTO
    {
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string[] Roles { get; set; } 
    }
}
