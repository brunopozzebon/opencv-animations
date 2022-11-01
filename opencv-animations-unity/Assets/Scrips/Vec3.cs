using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Vec3
    {
        public float x;
        public float y;
        public float z;

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public Vec3 copy()
        {
            return new Vec3(x, y, z);
        }
        
        public Vec3 sub(Vec3 otherVector)
        {
            x = x - otherVector.x;
            y = y - otherVector.y;
            z = z - otherVector.z;

            return this;
        }

        public Vector3 toVector3()
        {
            return new Vector3(
                x, y, z);
        }
    }
 
}