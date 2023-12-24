using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
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

        public async Task<Review?> UpdateOneReviewAsync(Guid id, Review review)
        {
            var existingReview = await _reviews.FindAsync(id);
            if (existingReview == null)
                return null;

            existingReview.Content = review.Content ?? existingReview.Content;

            if (review.Ratings != default)
            {
                existingReview.Ratings = review.Ratings;
            }

            _database.Update(existingReview);
            await _database.SaveChangesAsync();
            return existingReview;
        }
    }
}