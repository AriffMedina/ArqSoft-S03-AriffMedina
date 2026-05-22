using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;
using System.Text.Json;

namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonUserRepository : IUserRepository
    {
        private readonly string _filePath;

        public JsonUserRepository(string filePath)
        {
            _filePath = filePath;

            // Crear la carpeta si no existe (igual que items)
            var carpeta = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(carpeta))
                Directory.CreateDirectory(carpeta);

            // Crear el archivo vacío si no existe
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public List<Usuario> ObtenerTodos()
        {
            if (!File.Exists(_filePath))
                return new List<Usuario>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Usuario>>(json)
                   ?? new List<Usuario>();
        }

        public Usuario? ObtenerPorEmail(string email)
        {
            return ObtenerTodos()
                .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public Usuario? ObtenerPorNombreUsuario(string nombreUsuario)
        {
            return ObtenerTodos()
                .FirstOrDefault(u => u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase));
        }

        public void Agregar(Usuario usuario)
        {
            var usuarios = ObtenerTodos();

            // Auto-incrementar Id, igual que en items
            usuario.Id = usuarios.Count > 0
                         ? usuarios.Max(u => u.Id) + 1
                         : 1;

            usuarios.Add(usuario);
            Guardar(usuarios);
        }

        private void Guardar(List<Usuario> usuarios)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(usuarios, opciones);
            File.WriteAllText(_filePath, json);
        }
    }
}
