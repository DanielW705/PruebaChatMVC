namespace LibreriaChatMVC.Models
{
    public class Estatus
    {
        public Guid IdUsuario { get; set; }
        public required int IdEstatus { get; set; }
        public required DateTime UltimaConextion { get; set; }
        public EstadoDeConexion Rel_Estatus_Estado { get; set; } = null!;
        public Usuario Rel_Estatus_Usuario { get; set; } = null!;
    }
}
