using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineShopping.Filters
{
    public class MyAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        readonly string[] _requiredClaims;

        public MyAuthorizeAttribute(params string[] claims)
        {
            _requiredClaims = claims;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var hasAllRequredClaims = _requiredClaims.All(claim => context.HttpContext.User.HasClaim(x => x.Type == claim));
            if (!hasAllRequredClaims)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
