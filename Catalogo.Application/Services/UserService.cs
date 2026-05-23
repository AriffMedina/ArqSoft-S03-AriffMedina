using CatalogoApp.Domain.Models;
using CatalogoApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogoApp.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public string? Registrar(string nombreUsuario, string email, string password)
        {
            
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return "Todos los campos son obligatorios.";

            if (password.Length < 6)
                return "La contraseña debe tener al menos 6 caracteres.";

            if (_repo.ObtenerPorEmail(email) != null)
                return "Ya existe una cuenta con ese correo.";

            if (_repo.ObtenerPorNombreUsuario(nombreUsuario) != null)
                return "Ese nombre de usuario ya está en uso.";

            // Hashear la contraseña con BCrypt antes de guardar
            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                Email = email.ToLower(),
                PasswordHash = hash,
                FechaRegistro = DateTime.UtcNow
            };

            _repo.Agregar(usuario);
            return null; 
        }

        public Usuario? ValidarLogin(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var usuario = _repo.ObtenerPorEmail(email);
            if (usuario == null)
                return null;

            // Verificar el hash con BCrypt
            bool passwordCorrecta = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);
            return passwordCorrecta ? usuario : null;
        }
    }
}

