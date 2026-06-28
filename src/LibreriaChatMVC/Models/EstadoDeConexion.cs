namespace LibreriaChatMVC.Models
{
    public class EstadoDeConexion: SoftDelete
    {
        public required int Id { get; set; }
        public required string Sumary { get; set; }
        public required string Description { get; set; }
        public ICollection<Estatus>? Rel_Estado_Estatus { get; set; }
    }
}
