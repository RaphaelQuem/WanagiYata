using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Managers.Interactions
{
    public class Interaction
    {
        public string Id { get; set; }
        public CompletionCondition CompletionConditions { get; set; }
        public List<string> Texts { get; set; }
        public string SwitchTo { get; set; }
    }
}
