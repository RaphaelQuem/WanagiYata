using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Resource
{
    public enum DayTime
    {
        Day,
        Night
    }
    public enum ObjectState
    {
        Idle,
        Walking,
        SettingTrap,
        Shooting,
        Rolling,
        StealthKill
    }
    public enum EventAction
    {
        Walk,
        Talk,
        Animate
    }
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        None
    }
    public enum PlayerAction
    {
        Scalp,
        Talk,
        Skin,
        None
    }
}
