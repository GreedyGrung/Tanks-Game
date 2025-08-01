using TankGame.App.Utils.Enums;
using UnityEngine;

namespace TankGame.App.StaticData.Environment
{
    [CreateAssetMenu(fileName = "ObjectPoolData", menuName = "Static Data/Pool")]
    public class ObjectPoolStaticData : ScriptableObject
    {
        [SerializeField] private ObjectPoolTypeId _id;
        [SerializeField] private string _poolSceneName;
        [SerializeField] private int _poolSize;
        [SerializeField] private bool _autoExpand;

        public ObjectPoolTypeId Id => _id;
        public string PoolSceneName => _poolSceneName;
        public int PoolSize => _poolSize;
        public bool AutoExpand => _autoExpand;
    }
}