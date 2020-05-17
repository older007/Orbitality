using Newtonsoft.Json;
using UnityEngine;

namespace Utils
{
    public struct SerializableVector
    {
        public float Value1;
        public float Value2;
        public float Value3;

        public Vector3 ToVector()
        {
            return new Vector3(Value1, Value2, Value3);
        }
        public Color ToColor()
        {
            return new Color(Value1, Value2, Value3, 1);
        }

        public SerializableVector FromVector(Vector3 vector3)
        {
            Value1 = vector3.x;
            Value2 = vector3.y;
            Value3 = vector3.z;

            return this;
        }
        
        public SerializableVector FromColor(Color color)
        {
            Value1 = color.r;
            Value2 = color.g;
            Value3 = color.b;

            return this;
        }
    }
}