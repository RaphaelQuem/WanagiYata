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
        
        public static void AvoidCollision(this Vector3 vector,Vector3 origin, float range)
        {
            Quaternion rotation = Quaternion.AngleAxis(10, Vector3.up);
            while (Physics.Raycast(origin, vector, range))
                vector = rotation * vector;
            //bla bla
        }
    }
}
