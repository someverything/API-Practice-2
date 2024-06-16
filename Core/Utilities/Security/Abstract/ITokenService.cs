using Core.Entities.Concrete;

namespace Core.Utilities.Security.Abstract
{
    public interface ITokenService
    {
        Task<Token> CreateAccessTokenAsinc(AppUser appUser, List<string> roles);
        string CreateRefreshToken();
    }
}
