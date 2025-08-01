using TankGame.App.Infrastructure.Services.PersistentProgress.Data;
using TankGame.Core.Utils.Enums.Generated;
using UnityEngine;

namespace TankGame.App.Utils
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

        public static bool CompareTag(this GameObject gameObject, Tags tag)
            => gameObject.CompareTag(tag.ToString());
    }
}
