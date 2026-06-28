namespace LibreriaChatMVC.Models
{
    public class ChatGrupal : ChatBase
    {
        public ICollection<CatalogoIntegrantes>? Rel_Chats_Integrantes { get; set; }
    }
}
