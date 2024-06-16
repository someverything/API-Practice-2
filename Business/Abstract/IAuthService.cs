using Core.Utilities.Results.Abstract;
using Entities.DTOs.AuthDTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO model);
    }
}
