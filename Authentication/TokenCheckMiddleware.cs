using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using worksheet2.Model.Settings;
using worksheet2.Services;

namespace worksheet2.Authentication
{
    public class TokenCheckMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public TokenCheckMiddleware(
            RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        /**
         * checks the token and attach user to context
         */
        public async Task Invoke(
            HttpContext httpContext,
            IUserService userService,
            ITokenService tokenService)
        {
            var httpContextRequest = httpContext.Request;
            var stringValues = httpContextRequest.Headers["Authorization"];
            var token = stringValues.FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                UpdateHttpContext(httpContext, userService, tokenService, token);

            await _requestDelegate(httpContext);
        }

        private static void UpdateHttpContext(
            HttpContext context,
            IUserService userService,
            ITokenService tokenService,
            string token)
        {
            try
            {
                var userId = tokenService.GetUserIdFromToken(token);

                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetById(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}