using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Service.src.Shared;
using Ecom.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecom.WebAPI.src.Repository
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly DbSet<Review> _reviews;
        private readonly DbSet<User> _users;

        private readonly DataBaseContext _database;
        public ReviewRepo(DataBaseContext dataBase)
        {
            _reviews = dataBase.Review;
            _database = dataBase;
            _users = dataBase.Users;

        }

        public async Task<Review> CreateOneReviewAsync(Review review)
        {
            User user = await _users.FirstOrDefaultAsync((u) => u.Id == review.UserId) ?? throw CustomException.NotFoundException();

            review.Reviewer = user.FirstName;

            _reviews.Add(review);
            await _database.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteOneReviewAsync(Guid id)
        {
            var reviewToDelete = await _reviews.FindAsync(id);
            if (reviewToDelete == null)
                return false;

            _reviews.Remove(reviewToDelete);
            await _database.SaveChangesAsync();
            return true;
        }

        public Task<Review> GeteOneReviewAsync(Guid reviewId)
        {
            var foundReview = _reviews.FirstOrDefaultAsync((r) => r.Id == reviewId);
            return foundReview;
        }

        public async Task<Review?> UpdateOneReviewAsync(Review review)
        {

            _database.Update(review);
            await _database.SaveChangesAsync();
            return review;
        }
    }
}