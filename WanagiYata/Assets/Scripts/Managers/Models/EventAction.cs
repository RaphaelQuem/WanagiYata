using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Managers.Models
{
    public class EventAction
    {
        public string Target { get; set; }
        public int MovementX { get; set; }
        public int MovementY { get; set; }
        public string InteractionId { get; set; }

    }
}
