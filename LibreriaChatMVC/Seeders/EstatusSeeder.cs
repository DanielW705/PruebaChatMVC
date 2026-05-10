using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    public class EstatusSeeder : ISeeder<Estatus>
    {
        public Estatus[] ApplySeed() =>
            [
                new Estatus
                {
                    Id= 1,
                    IdEstatus = 1,
                    UltimaConextion = DateTime.Now.AddDays(-1)
                },
                new Estatus
                {
                    Id= 2,
                    IdEstatus = 1,
                    UltimaConextion = DateTime.Now.AddDays(-1)
                },
                new Estatus
                {
                    Id= 3,
                    IdEstatus = 1,
                    UltimaConextion = DateTime.Now.AddDays(-1)
                }
            ];
    }
}
