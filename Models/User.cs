using System;
using System.Collections.Generic;

namespace PruebaChatMVC.Models
{
    public class User : SoftDelete
    {
        public Guid id { get; set; }
        public string pasword;
        //[Required(ErrorMessage = "Es requerido este campo")]
        //[StringLength(10, ErrorMessage = "Debe ser de menos de 10 caracteres")]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "Es requerido la contraseña")]
        //[StringLength(3, ErrorMessage = "Debe ser menos de 3 caracteres")]
        public ICollection<UserChat> relChat_User1 { get; set; }
        public ICollection<UserChat> relChat_User2 { get; set; }
        public ICollection<Messages> relUser_Sender { get; set; }
        public ICollection<Messages> relUser_Reciber { get; set; }
    }
}
