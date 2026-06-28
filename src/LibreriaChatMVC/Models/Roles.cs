namespace LibreriaChatMVC.Models
{
    public class Roles : SoftDelete
    {
        public int ID { get; set; }
        public required string Sumary { get; set; }
        public required string Description { get; set; }
        public ICollection<Usuario>? Rel_Roles_Usuarios { get; set; }
    }
}
