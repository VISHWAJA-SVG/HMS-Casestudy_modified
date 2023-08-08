using Microsoft.AspNetCore.Identity;

namespace HotelManagement_backend.Repositories
{
    public interface ITokenRepository
    {
       string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
