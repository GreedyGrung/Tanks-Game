using TankGame.App.Entities.Interfaces;
using TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.App.StaticData.Environment;
using UnityEngine;
using Zenject;

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

        [Inject]
        private void Construct(IPoolsService poolsService)
        {
            PoolsService = poolsService;
        }

        public virtual void Initialize(ProjectileStaticData staticData)
        {
            _projectileStaticData = staticData;
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
                ReturnToPool();
            }
        }

        public virtual void Explode()
        {
            _projectileAnimation.gameObject.SetActive(true);
            _exploded = true;
            _visuals.SetActive(false);

            ReturnToPool();
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_projectileStaticData.Damage);
            }

            Explode();
        }

        public void OnSpawned()
        {
            _exploded = false;
            gameObject.SetActive(true);
            _projectileAnimation.gameObject.SetActive(false);
            _visuals.SetActive(true);
        }

        public void OnDespawned()
        {
            _timeFromSpawn = 0f;
            _visuals.SetActive(true);
        }

        public virtual void ReturnToPool() { }
    }
}
