using System.Security.Claims;
using Ecom.Core.src.Enum;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Ecom.WebAPI.Authorization
{
    public class OrderAdminOrOwnerHandler : AuthorizationHandler<AdminOrOwnerRequirement, OrderReadDTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrOwnerRequirement requirement, OrderReadDTO order)
        {
            var userRoleClaim = context.User.FindFirst(c => c.Type == ClaimTypes.Role);
            var userIdClaim = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);

            if (userRoleClaim == null || userIdClaim == null)
            {
                return Task.CompletedTask;
            }

            var userRole = userRoleClaim.Value;
            var userIdString = userIdClaim.Value;

            if (!Enum.TryParse<Role>(userRole, out var userRoleEnum))
            {
                return Task.CompletedTask;
            }

            if (userRoleEnum == Role.Admin)
            {
                context.Succeed(requirement);
            }
            else if (userIdString == order.User?.Id.ToString())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class AdminOrOwnerRequirement : IAuthorizationRequirement
    {
    }
}
