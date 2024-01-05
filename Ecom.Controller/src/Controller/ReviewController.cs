using System.Security.Claims;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService, IAuthorizationService authorizationService)
        {
            _reviewService = reviewService;
            _authorizationService = authorizationService;

        }

        [Authorize(Roles = "User")]
        [HttpPost()]
        public async Task<ActionResult<ReviewReadDTO>> CreateOneReviewAsync([FromBody] ReviewCreateDTO reviewCreateDTO)
        {

            if (reviewCreateDTO == null)
            {
                throw CustomException.BadRequestException("Invalid review data");
            }

            var userClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userClaim?.Value, out var userId))
            {
                throw CustomException.BadRequestException("Invalid user ID format");
            }

            if (userId != reviewCreateDTO.UserId)
            {
                throw CustomException.ForbiddenException("Reivew owner mismatch with the logged-in customer");
            }

            var result = await _reviewService.CreateOneReviewAsync(reviewCreateDTO);
            return Ok(result);
        }


        [Authorize]
        [HttpDelete("{reviewId}")]
        public async Task<ActionResult<bool>> DeleteOneReviewAsync(Guid reviewId)
        {
            var review = await _reviewService.GeteOneReviewAsync(reviewId) ?? throw CustomException.NotFoundException("Review with this id is not found");

            var authorizationResult = await _authorizationService
                 .AuthorizeAsync(User, review, "ReviewAdminOrOwnerPolicy");

            if (!authorizationResult.Succeeded)
            {
                throw CustomException.ForbiddenException("Only admin or onwer of the review can delete this review");
            }

            var createdReview = await _reviewService.DeleteOneReviewAsync(reviewId);
            return StatusCode(2014, createdReview);
        }


        [Authorize(Roles = "User")]
        [HttpPatch("{reviewId}")]
        public async Task<ActionResult<bool>> UpdateOneReviewAsync(Guid reviewId, ReviewUpdateDTO reviewUpdateDTO)
        {
            var review = await _reviewService.GeteOneReviewAsync(reviewId) ?? throw CustomException.NotFoundException("Review with this id is not found");

            var authorizationResult = await _authorizationService
                         .AuthorizeAsync(User, review, "ReviewOwnerPolicy");

            if (!authorizationResult.Succeeded)
            {
                throw CustomException.ForbiddenException("Not Authorized to update this review");
            }

            var updatedReview = await _reviewService.UpdateOneReviewAsync(reviewId, reviewUpdateDTO);
            return Ok(updatedReview);
        }
    }
}