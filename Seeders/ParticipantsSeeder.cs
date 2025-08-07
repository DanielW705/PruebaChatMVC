using PruebaChatMVC.Models;
using System.Collections.Generic;

namespace PruebaChatMVC.Seeders
{
    public class ParticipantsSeeder : ISeeder<Participants[], Users[], Chats>
    {
        public Participants[] ApplySeed(Users[] seed1, Chats seed2)
        {
            List<Participants> output = new List<Participants>();

            for (int i = 0; i < seed1.Length; i++)
            {
                output.Add(new Participants { IdParticipants = i + 1, IdChat = seed2.IdChat, IdUser = seed1[i].IdUser });
            }

            return output.ToArray();
        }
    }
}
