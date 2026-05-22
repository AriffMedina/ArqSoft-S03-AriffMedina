using System;
using System.Collections.Generic;
using System.Text;

using CatalogoApp.Domain.Models;

namespace CatalogoApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        List<Usuario> ObtenerTodos();
        Usuario? ObtenerPorEmail(string email);
        Usuario? ObtenerPorNombreUsuario(string nombreUsuario);
        void Agregar(Usuario usuario);
    }
}
