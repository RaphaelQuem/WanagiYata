using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public class EventModel
    {
        public string Target { get; set; }
        public int? DestinationX { get; set; }
        public int? DestinationY { get; set; }
        public bool WaitPrevious { get; set; }
    }
}
