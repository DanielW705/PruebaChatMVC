namespace LibreriaChatMVC.Models
{
    public class Usuario : SoftDelete
    {
        public required Guid IdUsuario { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required int Rol { get; set; }
        public required int Estatus { get; set; }
        public Roles? Rel_Usuario_Rol { get; set; }
        public Estatus? Rel_Usuario_Estatus { get; set; }
        public ICollection<CatalogoIntegrantes>? Rel_Usuario_Integrantes { get; set; }
        public ICollection<ChatIndividual>? Rel_Usuario_ChatIniciados { get; set; }
        public ICollection<ChatIndividual>? Rel_Usuario_ChatRecibidos { get; set; }
        public ICollection<Mensajes>? Rel_Usuario_Mensajes { get; set; }
    }
}
