using TankGame.App.Projectiles;
using UnityEngine;

namespace TankGame.App.Object_Pool
{
    public abstract class BaseProjectilePool : MonoBehaviour
    {
        [SerializeField] private int _poolSize;
        [SerializeField] private bool _autoExpand;

        public ObjectPool<Projectile> Pool { get; protected set; }
        public int PoolSize => _poolSize;
        public bool AutoExpand => _autoExpand;
    }
}
