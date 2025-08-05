using System;

namespace PruebaChatMVC.Models
{
    public abstract class SoftDelete
    {
        public bool isDelete {  get; set; }

        public DateTime Created { get; set; }
    }
}
