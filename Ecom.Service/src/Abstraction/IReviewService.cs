using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Abstraction
{
    public interface IReviewService
    {
        Task<ReviewReadDTO> CreateOneReviewAsync(ReviewCreateDTO ReviewCreateDTO);
        Task<IEnumerable<ReviewReadDTO>> GetAllReviewsAsync(GetAllParams options);
        Task<ReviewReadDTO> UpdateOneReviewAsync(Guid id, ReviewUpdateDTO reviewUpdateDTO);
        Task<bool> DeleteOneReviewAsync(Guid id);
    }
}