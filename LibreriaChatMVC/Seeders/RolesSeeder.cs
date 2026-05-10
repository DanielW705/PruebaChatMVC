using LibreriaChatMVC.Models;

namespace LibreriaChatMVC.Seeders
{
    public class RolesSeeder : ISeeder<Roles>
    {
        public Roles[] ApplySeed()
        {
            return
            [
                new Roles
                {
                    ID = 1,
                    Sumary = "ADMIN",
                    Description = "Usuario administrador"
                },new Roles
                {
                    ID = 2,
                    Sumary = "DEV",
                    Description = "Usuario Desarrollador"
                },
                new Roles
                {
                    ID = 3,
                    Sumary = "USER",
                    Description = "Usuario normal"
                }

            ];
        }
    }
}
