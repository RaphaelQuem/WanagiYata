using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public interface IState 
    {
         string Name { get; set; }
         Vector2 DirectorVector { get; set; }
         void Update();
    }
}
