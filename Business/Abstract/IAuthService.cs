using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.DTOs.AuthDTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO model);
        Task<DataResult<Token>> LoginAsync(LoginDTO model);
        Task<IDataResult<string>> UpdateRefreshTokenAsync(string refreshToken, AppUser appUser);
        Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken);
        Task<IResult> LogoutAsync(string userId);
    }
}
