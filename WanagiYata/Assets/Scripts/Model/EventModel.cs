using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public class EventModel
    {
        public string Target { get; set; }
        public float? DestinationX { get; set; }
        public float? DestinationY { get; set; }
        public string Interaction { get; set; }
        public bool WaitPrevious { get; set; }
    }
}
