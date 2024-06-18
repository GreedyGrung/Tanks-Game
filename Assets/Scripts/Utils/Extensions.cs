using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Extensions
    {
        public static T ToDeserizalized<T>(this string json)
            => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) 
            => JsonUtility.ToJson(obj);
    }
}
