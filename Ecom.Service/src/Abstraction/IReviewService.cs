using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface IReviewService
    {
        Task<ReviewReadDTO> CreateOneReviewAsync(ReviewCreateDTO ReviewCreateDTO);
        Task<ReviewReadDTO> UpdateOneReviewAsync(Guid reviewId, ReviewUpdateDTO reviewUpdateDTO);
        Task<ReviewReadDTO> GeteOneReviewAsync(Guid reviewId);
        Task<bool> DeleteOneReviewAsync(Guid reviewId);
    }
}