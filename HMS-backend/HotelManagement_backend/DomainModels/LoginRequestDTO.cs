using System.ComponentModel.DataAnnotations;

namespace HotelManagement_backend.DomainModels
{
    public class LoginRequestDTO
    {
        [DataType(DataType.EmailAddress)]
        public string Username { get;set; }
        [DataType(DataType.Password)]
        public string Password { get;set; }
    }
}
