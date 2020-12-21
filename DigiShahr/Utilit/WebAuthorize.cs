using System;
using System.Linq;
using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;



namespace DigiShahr.Utilit
{
    public class WebAuthorize : Attribute, IAuthorizationFilter
    {

        private string[] _actionaccess;

        public WebAuthorize(params string[] actionaccess)
        {
            _actionaccess = actionaccess;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsLoggedIn = context.HttpContext.User != null && context.HttpContext.User.Identity.IsAuthenticated;

            if (!IsLoggedIn)
            {
                throw new AuthenticationException("شما لاگین نکرده اید");
            }

            var userClaim = GetUserClaim(context.HttpContext);

            var userAccess = userClaim.Split(",");

            if (!_actionaccess.Any(p => userAccess.Contains(p)))
                throw new AuthenticationException("شما به این بخش دسترسی ندارید");

        }

        private static string GetUserClaim(HttpContext context)
        {
            return context.User.Claims.FirstOrDefault(p => p.Type == "UserRole")?.Value;
        }
    }
}
