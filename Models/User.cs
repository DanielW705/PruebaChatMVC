using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PruebaChatMVC.Utilities;

namespace PruebaChatMVC.Models
{
    public class User
    {
        public Guid id { get; set; }
        private string pasword;
        [Required(ErrorMessage = "Es requerido este campo")]
        [StringLength(10, ErrorMessage = "Debe ser de menos de 10 caracteres")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Es requerido la contraseña")]
        [StringLength(3, ErrorMessage = "Debe ser menos de 3 caracteres")]
        public string Pasword { get => pasword; set => pasword = value; }
        public UserChat relChat_User { get; set; }
        public User()
        {
        }
    }
}
