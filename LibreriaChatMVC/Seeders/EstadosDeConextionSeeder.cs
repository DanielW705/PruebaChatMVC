using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    public class EstadosDeConextionSeeder : ISeeder<EstadoDeConexion>
    {
        public EstadoDeConexion[] ApplySeed() =>
            [
                new EstadoDeConexion
                {
                    Id = 1,
                    Sumary ="DESCONECTADO",
                    Description ="USUARIO DESCONECTADO"
                },
                new EstadoDeConexion
                {
                    Id = 2,
                    Sumary ="CONECTADO",
                    Description ="USUARIO CONECTADO"
                },
                new EstadoDeConexion
                {
                    Id = 3,
                    Sumary ="NO MOLESTAR",
                    Description ="USUARIO MODO NO MOLESTAR"
                }
            ];
    }
}
