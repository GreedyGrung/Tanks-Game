using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.Interfaces;
using TankGame.App.StaticData;
using UnityEngine;

namespace TankGame.App.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IPoolableObject
    {
        [SerializeField] private ProjectileAnimation _projectileAnimation;
        [SerializeField] private GameObject _visuals;

        private ProjectileStaticData _projectileStaticData;
        private float _timeFromSpawn;
        private bool _exploded;

        protected IPoolsService PoolsService { get; private set; }

        public GameObject GameObjectRef => gameObject;

        public virtual void Initialize(ProjectileStaticData staticData, IPoolsService poolsService)
        {
            _projectileStaticData = staticData;
            PoolsService = poolsService;
        }

        public virtual void Update()
        {
            if (_exploded)
            {
                return;
            }

            transform.Translate(Vector3.right * _projectileStaticData.MoveSpeed * Time.deltaTime);
            _timeFromSpawn += Time.deltaTime;

            if (_timeFromSpawn >= _projectileStaticData.Lifetime)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _timeFromSpawn = 0f;
            _exploded = false;
            _visuals.SetActive(true);
            ReturnToPool();
        }

        public abstract void Explode();

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            _projectileAnimation.gameObject.SetActive(true);
            _exploded = true;
            _visuals.SetActive(false);

            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_projectileStaticData.Damage);
            }
        }

        public void OnSpawned()
        {
            gameObject.SetActive(true);
        }

        public void OnDespawned()
        {
            gameObject.SetActive(false);
        }

        public virtual void ReturnToPool()
        {
            
        }
    }
}
