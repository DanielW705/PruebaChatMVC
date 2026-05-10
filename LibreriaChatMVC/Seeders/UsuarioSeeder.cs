using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    public class UsuarioSeeder : ISeeder<Usuario, Estatus[]>
    {
        public Usuario[] ApplySeed(Estatus[] seed)
        {
            Usuario[] output = new Usuario[seed.Length];

            for (int i = 0; i < seed.Length; i++)
            {
                output[i] = new Usuario
                {
                    IdUsuario = Guid.NewGuid(),
                    UserName = string.Concat("Usuario ", i),
                    Password = "12345678",
                    Estatus = seed[i].Id,
                    Rol = (i + 1)
                };
            }
            return output;
        }
    }
}
