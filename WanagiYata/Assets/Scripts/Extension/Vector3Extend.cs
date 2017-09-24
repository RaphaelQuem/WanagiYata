using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Extension
{
    public static class Vector3Extend
    {
        public static Vector2 Extract2D(Vector3 v)
        {
            return new Vector2 { x = v.x, y = v.y };
        }
        public static Vector3 AvoidCollision(this Vector3 vector, Vector3 origin, float range, GameObject ignore = null)
        {

            Ray ray = new Ray(origin, vector);
            // Debug.DrawLine(origin, origin + (vector * 5), Color.red);
            int trys = 0;
            while (Physics2D.Raycast(origin, vector, 5) && trys < 10)
            {
                trys++;
                vector = Quaternion.AngleAxis(30, new Vector3(0, 0, 1)) * vector;
                Debug.Log(ignore.name + " colidiu " + vector.ToString());
            }

            return vector;
        }
        public static float ToAngleAxis(this Vector3 vector)
        {
            if (vector.x.Equals(-1))
                return 180;

            if (vector.y.Equals(1))
                return 90;

            if (vector.y.Equals(-1))
                return 270;

            return 0;
        }

    }
}
