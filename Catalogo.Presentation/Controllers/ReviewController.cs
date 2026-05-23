using CatalogoApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApp.Presentation.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public IActionResult Add(int itemId, string comment, int rating)
        {
            var userId = HttpContext.Session.GetInt32("UsuarioId");
            var userName = HttpContext.Session.GetString("UsuarioNombre");

            if (userId == null)
            {
                TempData["ReviewError"] = "You must be logged in to leave a review.";
                return RedirectToAction("Detail", "Catalogo", new { id = itemId });
            }

            var error = _reviewService.AddReview(itemId, userId.Value, userName!, comment, rating);
            if (error != null)
                TempData["ReviewError"] = error;

            return RedirectToAction("Detail", "Catalogo", new { id = itemId });
        }
    }
}