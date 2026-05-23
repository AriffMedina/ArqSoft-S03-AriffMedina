namespace CatalogoApp.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;       
        public string Raza { get; set; } = string.Empty;        
        public string Pelaje { get; set; } = string.Empty;       
        public string Temperamento { get; set; } = string.Empty; 
        public string Descripcion { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;    
    }
}