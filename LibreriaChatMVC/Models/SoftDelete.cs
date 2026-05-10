namespace LibreriaChatMVC.Models
{
    public abstract class SoftDelete
    {
        public bool IsDeleted { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
