using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mambo.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : System.Attribute, IAsyncAuthorizationFilter
    {
        private HashSet<string> RequiredRoles { get; set; } = new HashSet<string>();

        public CustomAuthorizeAttribute(params string[] requiredRoles)
        {
            foreach (var currentRequiredRole in requiredRoles)
            {
                RequiredRoles.Add(currentRequiredRole);
            }
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var roleClaimOfUser = context.HttpContext.User.Claims.FirstOrDefault(f => f.Type == "rolename")?.Value;
                if (roleClaimOfUser == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                if (RequiredRoles.Count > 0 && !RequiredRoles.Contains(roleClaimOfUser))
                {
                    context.Result = new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}