namespace LibreriaChatMVC.Models
{
    public abstract class SoftDelete
    {
        public bool IsDeleted { get; set; } = false;

        public DateTime CreateDate { get; set; }
        protected SoftDelete()
        {
            IsDeleted = false;
            CreateDate = DateTime.Now;
        }
    }
}
