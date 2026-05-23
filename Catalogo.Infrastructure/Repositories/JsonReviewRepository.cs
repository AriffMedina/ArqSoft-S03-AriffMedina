
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;
using System.Text.Json;

namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonReviewRepository : IReviewRepository
    {
        private readonly string _filePath;

        public JsonReviewRepository(string filePath)
        {
            _filePath = filePath;
            var folder = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(folder))
                Directory.CreateDirectory(folder);
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public List<Review> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Review>>(json) ?? new();
        }

        public List<Review> GetByItemId(int itemId) =>
            GetAll().Where(r => r.ItemId == itemId).ToList();

        public void Add(Review review)
        {
            var all = GetAll();
            review.Id = all.Count > 0 ? all.Max(r => r.Id) + 1 : 1;
            all.Add(review);
            File.WriteAllText(_filePath,
                JsonSerializer.Serialize(all, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}