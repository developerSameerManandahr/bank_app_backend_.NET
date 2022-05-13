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
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            {
                var filterContextHttpContext = filterContext.HttpContext;
                var user = (User) filterContextHttpContext.Items["User"];
                if (user != null) return;
                var message = new {message = "Unauthorized"};
                filterContext.Result = new JsonResult(message)
                    {StatusCode = StatusCodes.Status401Unauthorized};
            }
        }
    }
}