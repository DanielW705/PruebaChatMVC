namespace LibreriaChatMVC.Models
{
    public class ChatBase : SoftDelete
    {
        public required Guid IdChat { get; set; }
        public ICollection<Mensajes>? Rel_Mensajes_Chat { get; set; }

    }
}
