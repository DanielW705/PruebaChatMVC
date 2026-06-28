using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    public class EstatusSeeder : ISeeder<Estatus, Usuario[]>
    {
        public Estatus[] ApplySeed(Usuario[] seed) => seed.Select(u =>
        new Estatus
        {
            IdUsuario = u.IdUsuario,
            IdEstatus = 1,
            UltimaConextion = DateTime.Now.AddDays(-1)
        }).ToArray();

    }
}
