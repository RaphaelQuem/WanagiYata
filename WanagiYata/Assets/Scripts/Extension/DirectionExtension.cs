
using Assets.Scripts.Resource;
using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Extension
{
    static class DirectionExtension
    {
        public static Vector3 ToVector(this Direction dir)
        {
            Vector3 vector = Vector3.zero;
            switch (dir)
            {
                case Direction.Up:
                    vector = new Vector3(0,1);
                    break;
                case Direction.Right:
                    vector = new Vector3(1, 0);
                    break;
                case Direction.Down:
                    vector = new Vector3(0, -1);
                    break;
                case Direction.Left:
                    vector = new Vector3(-1,0 );
                    break;
            }
            return vector;
        }

    }
}
