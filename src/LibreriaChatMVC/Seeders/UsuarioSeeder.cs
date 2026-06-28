using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    public class UsuarioSeeder : ISeeder<Usuario>
    {
        public Usuario[] ApplySeed() => Enumerable.Range(0, 3)
            .Select(index => new Usuario
            {
                IdUsuario = Guid.NewGuid(),
                UserName = string.Concat("Usuario ", index),
                Password = "12345678",
                Rol = index + 1
            }).ToArray();

    }
}
