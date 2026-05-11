using System.ComponentModel.DataAnnotations;

namespace LibreriaChatMVC.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "Es necesario un nombre de usuario")]
        [StringLength(30, ErrorMessage = "Debe ser de menos de 30 caracteres")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Es necesario una contraseña")]
        [StringLength(16, ErrorMessage = "Debe ser menos de 3 caracteres", MinimumLength = 8)]
        public string? Password { get; set; }
    }
}
