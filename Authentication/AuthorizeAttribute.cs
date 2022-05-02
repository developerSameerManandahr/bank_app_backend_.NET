using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using worksheet2.Model;

namespace worksheet2.Authentication
{
    /**
     * Checks the controller methods that have [Authorize] appended before method
     * Checks if the token is valid by checking the user from the context
     */
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            {
                var user = (User) context.HttpContext.Items["User"];
                if (user == null)
                {
                    // not logged in
                    context.Result = new JsonResult(new {message = "Unauthorized"})
                        {StatusCode = StatusCodes.Status401Unauthorized};
                }
            }
        }
    }
}