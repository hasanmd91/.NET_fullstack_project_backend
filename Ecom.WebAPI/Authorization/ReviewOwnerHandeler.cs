using System.Security.Claims;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Ecom.WebAPI.Authorization
{
    public class ReviewOwnerHandeler : AuthorizationHandler<ReviewOwnerRequirement, ReviewReadDTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReviewOwnerRequirement requirement, ReviewReadDTO review)
        {
            var claimedUserId = context.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claimedUserId?.Value != null ? Guid.Parse(claimedUserId.Value) : Guid.Empty;

            if (userId == review.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class ReviewOwnerRequirement : IAuthorizationRequirement
    {
    }
}