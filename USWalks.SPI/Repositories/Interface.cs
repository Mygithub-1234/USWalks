using Microsoft.AspNetCore.Identity;

namespace USWalks.SPI.Repositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
