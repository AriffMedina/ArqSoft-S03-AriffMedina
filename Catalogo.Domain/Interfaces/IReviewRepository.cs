using CatalogoApp.Domain.Models;

namespace CatalogoApp.Domain.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> GetAll();
        List<Review> GetByItemId(int itemId);
        void Add(Review review);
    }
}