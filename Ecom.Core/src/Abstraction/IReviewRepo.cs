using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;

namespace Ecom.Core.src.Abstraction
{
    public interface IReviewRepo
    {
        Task<Review> CreateOneReviewAsync(Review review);
        Task<IEnumerable<Review>> GetAllReviewsAsync(GetAllParams options);
        Task<Review> UpdateOneReviewAsync(Guid id, Review review);
        Task<bool> DeleteOneReviewAsync(Guid id);
    }
}
