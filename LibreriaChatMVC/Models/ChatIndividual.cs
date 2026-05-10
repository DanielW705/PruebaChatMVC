namespace LibreriaChatMVC.Models
{
    public class ChatIndividual : ChatBase
    {
        public required Guid Emisor { get; set; }
        public required Guid Receptor { get; set; }
        public Usuario? Rel_Emisor_Chat { get; set; }
        public Usuario? Rel_Receptor_Chat { get; set; }
    }
}
