using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepo reviewRepo, IMapper mapper)
        {

            _reviewRepo = reviewRepo;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ReviewReadDTO>> GetAllReviewsAsync(GetAllParams options)
        {
            var reviews = await _reviewRepo.GetAllReviewsAsync(options);
            return _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewReadDTO>>(reviews);

        }
        public async Task<ReviewReadDTO> CreateOneReviewAsync(ReviewCreateDTO ReviewCreateDTO)
        {
            var reviewEntity = _mapper.Map<ReviewCreateDTO, Review>(ReviewCreateDTO);
            var createdReview = await _reviewRepo.CreateOneReviewAsync(reviewEntity);
            return _mapper.Map<Review, ReviewReadDTO>(createdReview);
        }

        public async Task<bool> DeleteOneReviewAsync(Guid reviewId)
        {
            return await _reviewRepo.DeleteOneReviewAsync(reviewId);
        }

        public async Task<ReviewReadDTO> UpdateOneReviewAsync(Guid reviewId, ReviewUpdateDTO reviewUpdateDTO)
        {
            var reviewEntity = _mapper.Map<ReviewUpdateDTO, Review>(reviewUpdateDTO);
            var updatedReview = await _reviewRepo.UpdateOneReviewAsync(reviewId, reviewEntity);
            return _mapper.Map<Review, ReviewReadDTO>(updatedReview);

        }
    }
}