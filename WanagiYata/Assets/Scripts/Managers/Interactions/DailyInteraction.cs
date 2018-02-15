using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Managers.Interactions
{
    public class DailyInteraction
    {
        public int DayNumber { get; set; }
        public List<Interaction> Interactions { get; set; }
    }
}
