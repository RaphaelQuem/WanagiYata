
using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Extension
{
    static class DirectionExtension
    {
        public static Vector3 ToVector(this FacingDirection dir)
        {
            Vector3 vector = Vector3.zero;
            switch (dir)
            {
                case FacingDirection.Up:
                    vector = new Vector3(0,1);
                    break;
                case FacingDirection.Right:
                    vector = new Vector3(1, 0);
                    break;
                case FacingDirection.Down:
                    vector = new Vector3(0, -1);
                    break;
                case FacingDirection.Left:
                    vector = new Vector3(-1,0 );
                    break;
            }
            return vector;
        }

    }
}
