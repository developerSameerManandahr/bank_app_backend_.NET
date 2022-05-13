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
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        /**
         * checks the token and attach user to context
         */
        public async Task Invoke(
            HttpContext context,
            IUserService userService,
            ITokenService tokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, userService, tokenService, token);

            await _next(context);
        }

        private static void AttachUserToContext(
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