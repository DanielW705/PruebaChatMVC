namespace LibreriaChatMVC.Models
{
    public class Mensajes : SoftDelete
    {
        public required Guid IdMensaje { get; set; }
        public required Guid IdEmisor { get; set; }
        public required Guid IdChat { get; set; }
        public string? Mensaje { get; set; }
        public DateTime? FechaModificado { get; set; }
        public DateTime? FechaEliminado { get; set; }
        public Usuario? Rel_Mensaje_Usuario { get; set; }
        public ChatBase? Rel_Mensaje_Chat { get; set; }
    }
}
