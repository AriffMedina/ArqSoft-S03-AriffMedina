using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApp.Presentation.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ItemService _service;
        private readonly ReviewService _reviewService;
        private readonly ItemService _itemService;

        // El servicio llega por inyección de dependencias
        public CatalogoController(ItemService service, ReviewService reviewService, ItemService itemService )
        {
            _service = service;
            _itemService = itemService;
            _reviewService = reviewService;
        }

        // Lista con filtro opcional por género
        public IActionResult Index(string? raza, string? pelaje, string? temperamento)
        {
            var items = _service.ObtenerTodos();

            if (!string.IsNullOrEmpty(raza))
                items = items.Where(i => i.Raza == raza).ToList();
            if (!string.IsNullOrEmpty(pelaje))
                items = items.Where(i => i.Pelaje == pelaje).ToList();
            if (!string.IsNullOrEmpty(temperamento))
                items = items.Where(i => i.Temperamento == temperamento).ToList();

            return View(items);
        }

        // Detalle de un item
        public IActionResult Detalle(int id)
        {
            var item = _service.ObtenerPorId(id);
            return item == null ? NotFound() : View(item);
        }

        // Formulario — GET
        public IActionResult Agregar()
        {
            return View();
        }

        // Formulario — POST
        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            _service.Agregar(item);
            return RedirectToAction("Index");
        }

        // Eliminar
        public IActionResult Eliminar(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var item = _itemService.GetById(id);
            if (item == null) return NotFound();

            ViewBag.Reviews = _reviewService.GetReviewsForItem(id);
            ViewBag.AverageRating = _reviewService.GetAverageRating(id);

            return View(item);
        }
    }
}