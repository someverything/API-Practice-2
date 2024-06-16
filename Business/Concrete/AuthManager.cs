using Business.Abstract;
using Business.Validations.FluentValidation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Entities.Common;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Business.Concrete
{
    internal class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthManager(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IResult> RegisterAsync(RegisterDTO model)
        {
            var validator = new RegisterValidation();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
                return new ErrorResult(message: validationResult.ToString(), HttpStatusCode.BadRequest);

            User newUser = new()
            {
                FristName = model.FirstName,
                Email = model.Email,
                LastName = model.LastName,
                UserName = model.Username,

            };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
                return new SuccessResult(System.Net.HttpStatusCode.Created);
            else
            {
                string response = string.Empty;
                foreach (var error in result.Errors)
                {
                    response += error.Description + ". ";
                }
                return new ErrorResult(response, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
