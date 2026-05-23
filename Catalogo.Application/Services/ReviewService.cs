using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Application.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _repo;

        public ReviewService(IReviewRepository repo) => _repo = repo;

        public List<Review> GetReviewsForItem(int itemId) =>
            _repo.GetByItemId(itemId)
                 .OrderByDescending(r => r.CreatedAt)
                 .ToList();

        public double GetAverageRating(int itemId)
        {
            var reviews = _repo.GetByItemId(itemId);
            return reviews.Count == 0 ? 0 : Math.Round(reviews.Average(r => r.Rating), 1);
        }

        
        public string? AddReview(int itemId, int userId, string userName, string comment, int rating)
        {
            if (string.IsNullOrWhiteSpace(comment))
                return "Comment cannot be empty.";
            if (rating < 1 || rating > 5)
                return "Rating must be between 1 and 5.";

            _repo.Add(new Review
            {
                ItemId = itemId,
                UserId = userId,
                UserName = userName,
                Comment = comment.Trim(),
                Rating = rating,
                CreatedAt = DateTime.UtcNow
            });
            return null;
        }
    }
}