using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Vec2
    {
        public float x;
        public float y;

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        
        public Vec2 copy()
        {
            return new Vec2(x, y);
        }
        
        public Vec2 sub(Vec2 otherVector)
        {
            x = x - otherVector.x;
            y = y - otherVector.y;

            return this;
        }
        
        public Vec2 add(Vec2 otherVector)
        {
            x = x + otherVector.x;
            y = y + otherVector.y;

            return this;
        }

        public float dot(Vec2 otherVector)
        {
            return x * otherVector.x + y * otherVector.y;
        }
           

        public float angleBetweenLine(Vec2 otherVector)
        {
            float dotProduct = dot(otherVector);
            float modA = length();
            float modB = otherVector.length();

            float division = dotProduct / (modA * modB);
            return Mathf.Acos(division);
        }

        public float length()
        {
            return Mathf.Sqrt(x * x + y * y);
        }
        
        public float distanceTo(Vec2 other)
        {
            return Mathf.Sqrt(
                Mathf.Pow(this.x - other.x, 2) +
                Mathf.Pow(this.y - other.y, 2)
            );
        }

        public Vec2 invert()
        {
            this.x = -this.x;
            this.y = -this.y;
            return this;
        }

        public Vec2 normalize()
        {
            float factor = length();
            this.x = this.x / factor;
            this.y = this.y / factor;
            return this;
        }

        public Vec2 scale(float scalar)
        {
            this.x = this.x * scalar;
            this.y = this.y * scalar;
            return this;
        }
    }
}