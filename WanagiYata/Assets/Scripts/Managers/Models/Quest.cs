using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Managers.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string Issuer { get; set; }
        public int Scalps { get; set; }
        public int Skins { get; set; }

        public static Quest ById(int? questId)
        {
            //mock
            return new Quest
            {
                Id = 1,
                Issuer = "Shilah",
                Scalps = 3,
                Skins = 2
            };
        }
    }
}
