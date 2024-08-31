using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Extensions
    {
        public static T ToDeserizalized<T>(this string json)
            => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) 
            => JsonUtility.ToJson(obj);

        public static Vector3Data AsVectorData(this Vector3 vector) 
            => new(vector.x, vector.y, vector.z);

        public static Vector3 AsUnityVector3(this Vector3Data vector)
            => new(vector.X, vector.Y, vector.Z);
    }
}
