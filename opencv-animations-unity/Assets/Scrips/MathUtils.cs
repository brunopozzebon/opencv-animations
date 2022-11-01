using UnityEngine;

namespace DefaultNamespace
{
    public class MathUtils
    {
        public static float toDegress(float radians)
        {
            return radians * 180 / Mathf.PI;
        }
    }
}