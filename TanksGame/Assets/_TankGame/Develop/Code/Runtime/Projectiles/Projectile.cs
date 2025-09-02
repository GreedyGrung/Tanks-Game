using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Infrastructure.Services.Pause;
using TankGame.Runtime.Infrastructure.Services.PoolsService;
using TankGame.Runtime.StaticData.Environment;
using UnityEngine;
using Zenject;

namespace TankGame.Runtime.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IPoolableObject, IPausable
    {
        [SerializeField] private ProjectileAnimation _projectileAnimation;
        [SerializeField] private GameObject _visuals;

        private ProjectileStaticData _projectileStaticData;
        private float _timeFromSpawn;
        private bool _exploded;

        protected IPoolsService PoolsService { get; private set; }

        protected bool IsPaused { get; private set; }

        [Inject]
        private void Construct(IPoolsService poolsService) => PoolsService = poolsService;

        private void Start() => _projectileAnimation.OnFinished += ReturnToPool;

        protected void Update()
        {
            if (_exploded || IsPaused)
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

        private void OnDestroy() => _projectileAnimation.OnFinished -= ReturnToPool;

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_projectileStaticData.Damage);
            }

            Explode();
        }

        public void Initialize(ProjectileStaticData staticData) 
            => _projectileStaticData = staticData;

        protected virtual void ReturnToPool() { }

        public void Explode()
        {
            _projectileAnimation.gameObject.SetActive(true);
            _exploded = true;
            _visuals.SetActive(false);
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
            gameObject.SetActive(false);
        }

        public void SetIsPaused(bool value)
        {
            IsPaused = value;
        }
    }
}
