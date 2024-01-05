using System.Security.Claims;
using Ecom.Core.src.Enum;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Ecom.WebAPI.Authorization
{
    public class ReviewAdminOrOwnerHandeler : AuthorizationHandler<ReviewAdminOrOwnerRequirement, ReviewReadDTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReviewAdminOrOwnerRequirement requirement, ReviewReadDTO review)
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
            else if (userIdString == review.User?.Id.ToString())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class ReviewAdminOrOwnerRequirement : IAuthorizationRequirement
    {
    }
}