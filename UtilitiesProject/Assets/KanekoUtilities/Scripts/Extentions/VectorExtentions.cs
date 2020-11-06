using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public static class VectorExtentions
    {
        public static float Angle(this Vector2 self)
        {
            return Mathf.Atan2(self.x, self.y) * Mathf.Rad2Deg;
        }
    }
}