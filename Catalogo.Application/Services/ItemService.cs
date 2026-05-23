using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Application.Services
{
    public class ItemService
    {
        private readonly IItemRepository _repo;

        public ItemService(IItemRepository repo)
        {
            _repo = repo;
        }

        public List<Item> ObtenerTodos() => _repo.ObtenerTodos();

        public Item? GetById(int id) =>
            _repo.ObtenerTodos().FirstOrDefault(i => i.Id == id);

        public Item? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

        public void Agregar(Item item) => _repo.Agregar(item);

        public void Eliminar(int id) => _repo.Eliminar(id);

        // ── Filtros por categorías de gatos ──────────────────────────

        public List<Item> ObtenerPorRaza(string raza) =>
            _repo.ObtenerTodos()
                 .Where(i => i.Raza == raza)
                 .ToList();

        public List<Item> ObtenerPorPelaje(string pelaje) =>
            _repo.ObtenerTodos()
                 .Where(i => i.Pelaje == pelaje)
                 .ToList();

        public List<Item> ObtenerPorTemperamento(string temperamento) =>
            _repo.ObtenerTodos()
                 .Where(i => i.Temperamento == temperamento)
                 .ToList();

        // ── Listas para poblar los dropdowns ─────────────────────────

        public List<string> ObtenerRazas() =>
            _repo.ObtenerTodos().Select(i => i.Raza).Distinct().OrderBy(r => r).ToList();

        public List<string> ObtenerPelajes() =>
            _repo.ObtenerTodos().Select(i => i.Pelaje).Distinct().OrderBy(p => p).ToList();

        public List<string> ObtenerTemperamentos() =>
            _repo.ObtenerTodos().Select(i => i.Temperamento).Distinct().OrderBy(t => t).ToList();
    }
}