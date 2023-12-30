using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Shared;

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


        public async Task<ReviewReadDTO> CreateOneReviewAsync(ReviewCreateDTO ReviewCreateDTO)
        {
            var reviewEntity = _mapper.Map<ReviewCreateDTO, Review>(ReviewCreateDTO);
            var createdReview = await _reviewRepo.CreateOneReviewAsync(reviewEntity) ?? throw CustomException.BadRequestException();
            return _mapper.Map<Review, ReviewReadDTO>(createdReview);
        }

        public async Task<bool> DeleteOneReviewAsync(Guid reviewId)
        {
            var result = await _reviewRepo.DeleteOneReviewAsync(reviewId);
            if (!result)
            {
                throw CustomException.NotFoundException();
            }
            return result;
        }

        public async Task<ReviewReadDTO> UpdateOneReviewAsync(Guid reviewId, ReviewUpdateDTO reviewUpdateDTO)
        {
            var reviewToUpdate = await _reviewRepo.GeteOneReviewAsync(reviewId) ?? throw CustomException.NotFoundException();
            var updateReview = _mapper.Map(reviewUpdateDTO, reviewToUpdate);
            var updatedReview = await _reviewRepo.UpdateOneReviewAsync(updateReview);
            return _mapper.Map<Review, ReviewReadDTO>(updatedReview);

        }
    }
}