using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Managers.Models
{
    public class PlayerEvent
    {
        public int Id { get; set; }
        public List<EventAction> Actions {get;set;}

    }
}
