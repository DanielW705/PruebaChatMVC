using System.ComponentModel.DataAnnotations;

namespace PruebaChatMVC.ViewModel
{
    public class LoginRegisterUserViewModel
    {
        [Required(ErrorMessage = "Es requerido este campo")]
        [StringLength(10, ErrorMessage = "Debe ser de menos de 10 caracteres")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Es requerido la contraseña")]
        [StringLength(3, ErrorMessage = "Debe ser menos de 3 caracteres")]
        public string Pasword { get; set; }
    }
}
