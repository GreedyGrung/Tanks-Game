using TankGame.App.Interfaces;
using UnityEngine;

namespace TankGame.App.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10;
        [SerializeField] private float _damage;
        [SerializeField] private float _lifetime;
        [SerializeField] private ProjectileAnimation _projectileAnimation;
        [SerializeField] private GameObject _visuals;

        private float _timeFromSpawn;
        private bool _exploded;

        public virtual void Update()
        {
            if (_exploded)
            {
                return;
            }

            transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
            _timeFromSpawn += Time.deltaTime;

            if (_timeFromSpawn >= _lifetime)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _timeFromSpawn = 0f;
            _exploded = false;
            _visuals.SetActive(true);
        }

        public abstract void Explode();

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            _projectileAnimation.gameObject.SetActive(true);
            _exploded = true;
            _visuals.SetActive(false);

            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}
