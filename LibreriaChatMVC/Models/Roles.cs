namespace LibreriaChatMVC.Models
{
    public class Roles : SoftDelete
    {
        public required int ID { get; set; }
        public required string Sumary { get; set; }
        public required string Description { get; set; }
        public ICollection<Usuario>? Rel_Roles_Usuarios { get; set; }
    }
}
