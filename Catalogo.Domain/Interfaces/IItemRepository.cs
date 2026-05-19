using CatalogoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogoApp.Domain.Interfaces
{
    public interface IItemRepository
    {
        List<Item> ObtenerTodos();
        Item? ObtenerPorId(int id);
        void Agregar(Item item);
        void Eliminar(int id);
    }
}
