using PruebaChatMVC.Models;
using System;

namespace PruebaChatMVC.Seeders
{
    public class UserSeeder : ISeeder<User[]>
    {
        public User[] ApplySeed()
        {
            return [
                new User { id = Guid.Parse("bacab9dc-ba71-49bf-be42-99dfeda0504c"), pasword = "123", UserName = "Daniel"},
                new User {id = Guid.Parse("cc397046-0bb8-4406-8eb0-d5795ed51512"), pasword = "456", UserName = "Julio"}
            ];
        }
    }
}
