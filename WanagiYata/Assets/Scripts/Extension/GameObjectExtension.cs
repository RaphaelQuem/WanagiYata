using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Extension
{
    public static class GameObjectExtension
    {
        public static GameObject GetClosestObject(this GameObject self, GameObject[] objects)
        {
            GameObject result = null;
            float minDist = Mathf.Infinity;
            foreach (GameObject o in objects)
            {
                float dist = Vector3.Distance(o.transform.position, self.transform.position);

                if (dist < minDist)
                {
                    result = o;
                    minDist = dist;
                }

            }
            return result;
        }
    }
}
