namespace LibreriaChatMVC.Models
{
    public class CatalogoIntegrantes
    {
        public required int Id { get; set; }
        public required Guid IdUsuario { get; set; }
        public required Guid IdChat { get; set; }
        public required DateTime FechaDeIngreso { get; set; }
        public DateTime? FechaDeExpulsion { get; set; }
        public ChatGrupal? Rel_Integrantes_Chat { get; set; }
        public Usuario? Rel_Integrantes_Usuario { get; set; }
    }
}