using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Managers.Interactions
{
    public class NPCInteractionManager
    {
        public string NPCName { get; set; }
        public List<DailyInteraction> DailyInteractions { get; set; }
        public string CurrentInteractionId { get; set; }
        public int InteractionNumber { get; set; }

    }
}
