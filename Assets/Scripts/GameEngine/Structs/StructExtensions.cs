using UnityEngine;

namespace GameEngine.Structs
{
    public static class StructExtensions
    {
        public static Vector3 ToVector(this float3 val)
        {
            return new Vector3(val.x, val.y, val.z);
        }

        public static float3 ToFloat3(this Vector3 vector)
        {
            return new float3(vector);
        }
    }
}