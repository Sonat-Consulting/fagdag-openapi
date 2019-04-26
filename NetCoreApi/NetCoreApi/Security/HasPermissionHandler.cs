using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreApi.Security
{
    public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirements requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == "permissions" && claim.Issuer == requirement.Issuer))
            {
                return Task.CompletedTask;
            }

            var permissions =
                context.User.FindAll(claim => claim.Type == "permissions" && claim.Issuer == requirement.Issuer).Select(claim => claim.Value);
            
            if (permissions.Contains(requirement.Scope))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}