
using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Extension
{
    static class DirectionExtension
    {
        public static Vector3 ToVector(this PlayerDirection dir)
        {
            Vector3 vector = Vector3.zero;
            switch (dir)
            {
                case PlayerDirection.Up:
                    vector = new Vector3(0,1);
                    break;
                case PlayerDirection.Right:
                    vector = new Vector3(1, 0);
                    break;
                case PlayerDirection.Down:
                    vector = new Vector3(0, -1);
                    break;
                case PlayerDirection.Left:
                    vector = new Vector3(-1,0 );
                    break;
            }
            return vector;
        }

    }
}
