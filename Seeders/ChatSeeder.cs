using PruebaChatMVC.Models;
using System;
using System.Collections.Generic;

namespace PruebaChatMVC.Seeders
{
    public class ChatSeeder : ISeeder<UserChat[], User[]>
    {
        public UserChat[] ApplySeed(User[] users)
        {
            List<UserChat> output = new List<UserChat>();
            for (int i = 0; i < users.Length; i++)
            {
                for (int j = i + 1; j < users.Length; j++)
                {
                    output.Add(new UserChat { IdChat = i + 1, idUser1 = users[i].id, idUser2 = users[j].id });
                }
            }

            return output.ToArray();
        }
    }
}
