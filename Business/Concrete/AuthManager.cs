using Azure;
using Business.Abstract;
using Business.Messages;
using Business.Validations.FluentValidation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Core.Utilities.Security.Abstract;
using Entities.Common;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Net;

namespace Business.Concrete
{
    internal class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        public async Task<DataResult<Token>> LoginAsync(LoginDTO model)
        {
            var findUser = await _userManager.FindByIdAsync(model.UsernameOrEmail);
            if (findUser == null)
                findUser = await _userManager.FindByNameAsync(model.UsernameOrEmail);

            if (findUser == null)
            {
                //todo why we sende token here
                Log.Error("User doesnt exist");
                return new ErrorDataResult<Token>(message: "User doesnt exist!", HttpStatusCode.NotFound);
            }
            //todo it block forever or it givers time
            var result = await _signInManager.CheckPasswordSignInAsync(findUser, model.Password, false);
            var userRoles = await _userManager.GetRolesAsync(findUser);

            if (result.Succeeded)
            {
                Token token = await _tokenService.CreateAccessTokenAsinc(findUser, userRoles.ToList());
                var response = await UpdateRefreshTokenAsync(token.RefreshToken, findUser);
                return new SuccessDataResult<Token>(data: token,statusCode: HttpStatusCode.OK, message: response.Message);
            }
            else
            {
                Log.Error("Username or passwor is incorrect!");
                return new ErrorDataResult<Token>("Username or password is incorrect", HttpStatusCode.BadRequest);
            }

        }

        public async Task<IResult> LogoutAsync(string userId)
        {
            var user = await _userManager.FindByNameAsync(userId);
            if (user is not null)
            {
                user.RefreshToken = null;
                user.RefreshtokenExpiredDate = null;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded) return new SuccessResult(statusCode: HttpStatusCode.OK);
                else
                {
                    string response = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        response += error.Description + ". ";
                    }
                    Log.Error(response);
                    return new ErrorResult(response, HttpStatusCode.BadRequest);
                }
            }
            return new ErrorResult(HttpStatusCode.Unauthorized);
        }

        public async Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
            var userRoles = await _userManager.GetRolesAsync(user);
            if(user is not null && user.RefreshtokenExpiredDate > DateTime.Now)
            {
                Token token = await _tokenService.CreateAccessTokenAsinc(user, userRoles.ToList());
                token.RefreshToken = refreshToken;
                return new SuccessDataResult<Token>(data: token, statusCode: HttpStatusCode.OK);
            }
            else
            {
                Log.Error("Session is over. Login again!");
                return new ErrorDataResult<Token>(statusCode: HttpStatusCode.BadRequest, message: "Session is over. Login again!");
            }
        }

        public async Task<IResult> RegisterAsync(RegisterDTO model)
        {
            var validator = new RegisterValidation();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                Log.Error(validationResult.ToString());
                return new ErrorResult(message: validationResult.ToString(), HttpStatusCode.BadRequest);
            }

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

        public async Task<IDataResult<string>> UpdateRefreshTokenAsync(string refreshToken, AppUser appUser)
        {
            if(appUser is not null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshtokenExpiredDate = DateTime.Now.AddMonths(1);
                var res = await _userManager.UpdateAsync(appUser);
                if (res.Succeeded)
                {
                    return new SuccessDataResult<string>(data: refreshToken, HttpStatusCode.OK);
                }
                else
                {
                    string response = string.Empty;
                    foreach (var error in res.Errors)
                    {
                        response += error.Description + ". ";
                    }
                    Log.Error(response);
                    return new ErrorDataResult<string>(message: response, HttpStatusCode.BadRequest);
                }
            }
            else
            {
                Log.Error(HttpStatusCode.NotFound.ToString());
                return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            }
        }
    }
}
