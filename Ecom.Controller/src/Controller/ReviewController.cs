using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpPost()]
        public async Task<ActionResult<ReviewReadDTO>> CreateOneReviewAsync([FromBody] ReviewCreateDTO reviewCreateDTO)
        {
            var createdReview = await _reviewService.CreateOneReviewAsync(reviewCreateDTO);
            return Ok(createdReview);
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult<bool>> DeleteOneReviewAsync(Guid reviewId)
        {
            var createdReview = await _reviewService.DeleteOneReviewAsync(reviewId);
            return StatusCode(2014, createdReview);
        }


        [HttpPatch("{reviewId}")]
        public async Task<ActionResult<bool>> UpdateOneReviewAsync(Guid reviewId, ReviewUpdateDTO reviewUpdateDTO)
        {
            var createdReview = await _reviewService.UpdateOneReviewAsync(reviewId, reviewUpdateDTO);
            return Ok(createdReview);
        }
    }
}