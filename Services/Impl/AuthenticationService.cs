using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using worksheet2.Data;
using worksheet2.Data.Repository;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;
using worksheet2.Model.Settings;

namespace worksheet2.Services.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IAccountDetailRepository _accountDetailRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;

        public AuthenticationService(
            IUserRepository userRepository,
            IAccountDetailRepository accountDetailRepository,
            IUserDetailsRepository userDetailsRepository,
            IOptions<AppSettings> appOptions
        )
        {
            _userRepository = userRepository;
            _accountDetailRepository = accountDetailRepository;
            _userDetailsRepository = userDetailsRepository;
            _appSettings = appOptions.Value;
        }

        public AuthenticationResponse Authenticate(AuthenticateRequest request)
        {
            var user = _userRepository.GetUserByUsername(request.Username);

            if (user == null || !Crypto.VerifyHashedPassword(user.Password, request.Password)) return null;

            var token = GenerateJwtToken(user);

            return new AuthenticationResponse(user, token);
        }

        public AuthenticationResponse AuthenticateByPin(AuthenticatePinRequest request)
        {
            var user = _userRepository.GetUserByUsername(request.AccountNumber);

            if (user == null || !Crypto.VerifyHashedPassword(user.Pin, request.Pin)) return null;

            var token = GenerateJwtToken(user);

            return new AuthenticationResponse(user, token);
        }

        public BaseResponse VerifyPin(VerifyPinRequest verifyPinRequest, User user)
        {
            var userFromContext = _userRepository.GetUserByAccountNumber(user.AccountNumber);
            ;

            if (userFromContext != null && Crypto.VerifyHashedPassword(userFromContext.Pin, verifyPinRequest.Pin))
                return new BaseResponse("PIN is correct", "Success");

            return new BaseResponse("PIN is incorrect", "Error");
        }

        public BaseResponse SignUp(SignupRequest request)
        {
            var user = new User
            {
                Password = Crypto.HashPassword(request.Password),
                Pin = Crypto.HashPassword(request.Pin),
                UserName = request.Username,
                AccountNumber = GenerateAccount(10)
            };
            var createdUser = _userRepository.CreateUser(user);
            var userDetails = new UserDetails
            {
                Address = request.Address,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                User = createdUser,
                PhoneNumber = request.PhoneNumber
            };
            _userDetailsRepository.CreateUserDetails(userDetails);

            CreateAccountDetails(createdUser, AccountType.CURRENT);
            CreateAccountDetails(createdUser, AccountType.SAVING);

            return new BaseResponse(
                "Signup Successful",
                "Success"
            );
        }

        private void CreateAccountDetails(User createdUser, AccountType accountType)
        {
            var accountDetails = new AccountDetails
            {
                Balance = 0,
                Currency = "GBP",
                User = createdUser,
                AccountType = accountType
            };
            _accountDetailRepository.CreateAccountDetail(accountDetails);
        }


        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.UserId)}),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TimeOut),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateAccount(int length)
        {
            var random = new Random();
            var accountNumber = string.Empty;
            for (var i = 0; i < length; i++)
                accountNumber = string.Concat(accountNumber, random.Next(10).ToString());
            var user = _userRepository.GetUserByAccountNumber(accountNumber);
            if (user == null)
                return accountNumber;
            return GenerateAccount(length);
        }
    }
}