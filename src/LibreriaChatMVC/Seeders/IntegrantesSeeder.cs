using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    public class IntegrantesSeeder : ISeeder<CatalogoIntegrantes, Usuario[], Guid>
    {
        public CatalogoIntegrantes[] ApplySeed(Usuario[] seed1, Guid seed2) => [.. seed1.Select((s, index) => new CatalogoIntegrantes { Id = (index + 1), IdChat = seed2, IdUsuario = s.IdUsuario, FechaDeIngreso = DateTime.Now })];
    }
}
