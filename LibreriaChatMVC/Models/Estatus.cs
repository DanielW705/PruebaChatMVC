namespace LibreriaChatMVC.Models
{
    public class Estatus
    {
        public int Id { get; set; }
        public required int IdEstatus { get; set; }
        public required DateTime UltimaConextion { get; set; }
        public EstadoDeConexion? Rel_Estatus_Estado { get; set; }
        public Usuario? Rel_Estatus_Usuario { get; set; }
    }
}
