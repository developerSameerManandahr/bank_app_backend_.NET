using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using worksheet2.Data;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;
using worksheet2.Model.Settings;

namespace worksheet2.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly BankContext _context;

        private readonly AppSettings _appSettings;

        public UserService(
            BankContext context,
            IOptions<AppSettings> appOptions
        )
        {
            this._context = context;
            this._appSettings = appOptions.Value;
        }

        public AuthenticationResponse Authenticate(AuthenticateRequest request)
        {
            var user = _context.Users
                .Include(u => u.UserDetails)
                .FirstOrDefault(user => user.UserName == request.Username);

            if (user == null || !Crypto.VerifyHashedPassword(user.Password, request.Password))
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new AuthenticationResponse(user, token);
        }

        public AuthenticationResponse AuthenticateByPin(AuthenticatePinRequest request)
        {
            var user = _context.Users
                .Include(u => u.UserDetails)
                .FirstOrDefault(user => user.AccountNumber == request.AccountNumber);

            if (user == null || !Crypto.VerifyHashedPassword(user.Pin, request.Pin))
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new AuthenticationResponse(user, token);
        }

        public BaseResponse SignUp(SignupRequest request)
        {
            var user = new User()
            {
                Password = Crypto.HashPassword(request.Password),
                Pin = Crypto.HashPassword(request.Pin),
                UserName = request.Username,
                AccountNumber = GenerateAccount(10)
            };
            var userEntry = _context.Users
                .Add(user);
            var createdUser = userEntry.Entity;
            var userDetails = new UserDetails()
            {
                Address = request.Address,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                User = createdUser
            };
            _context.UserDetails
                .Add(userDetails);
            _context.SaveChanges();

            CreateAccountDetails(createdUser, AccountType.SAVING);

            return new BaseResponse(
                "Signup Successful",
                "Success"
            );
        }

        private void CreateAccountDetails(User createdUser, AccountType accountType)
        {
            _context.AccountDetails
                .Add(new AccountDetails()
                {
                    Balance = 0,
                    Currency = "GBP",
                    User = createdUser,
                    AccountType = accountType
                });
            _context.SaveChanges();

        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(u => u.UserDetails)
                .ToList();
        }

        public User GetById(string id)
        {
            return _context.Users
                .Include(user => user.UserDetails)
                .FirstOrDefault(user => user.UserId == id);
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
            var user = _context.Users
                .FirstOrDefault(user => user.AccountNumber == accountNumber);
            if (user == null)
            {
                return accountNumber;
            }
            else
            {
                return GenerateAccount(length);
            }
        }
    }
}