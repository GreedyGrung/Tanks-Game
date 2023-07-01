using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifetime;

    private float _timeFromSpawn;

    public virtual void Update()
    {
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
    }

    public abstract void Explode();

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
        }

        gameObject.SetActive(false);
    }
}
