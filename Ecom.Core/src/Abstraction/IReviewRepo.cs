using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface IReviewRepo
    {
        Task<Review> CreateOneReviewAsync(Review review);
        Task<Review> GeteOneReviewAsync(Guid reviewId);
        Task<Review> UpdateOneReviewAsync(Review review);
        Task<bool> DeleteOneReviewAsync(Guid id);
    }
}
