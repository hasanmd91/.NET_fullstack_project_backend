using System.Security.Claims;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Ecom.WebAPI.Authorization
{
    public class OrderOwnerHandler : AuthorizationHandler<OrderOwnerRequirement, OrderReadDTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OrderOwnerRequirement requirement, OrderReadDTO order)
        {
            var claimedUserId = context.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claimedUserId?.Value != null ? Guid.Parse(claimedUserId.Value) : Guid.Empty;

            if (userId == order?.User?.Id)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }

    public class OrderOwnerRequirement : IAuthorizationRequirement
    {
    }
}