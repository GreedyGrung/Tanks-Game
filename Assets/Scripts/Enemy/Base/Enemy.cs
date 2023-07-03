using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Transform _bulletSpawn;

    public StateMachine StateMachine { get; private set; }
    public Transform Player { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public BaseProjectilePool ProjectilePool { get; protected set; }
    public Projectile Projectile { get; protected set; }

    public EnemyData EnemyData => _enemyData;
    public Transform BulletSpawn => _bulletSpawn;

    public bool CanShoot { get; private set; } = true;
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public virtual void Start()
    {
        StateMachine = new();
        MaxHealth = _enemyData.MaxHealth;
        CurrentHealth = MaxHealth;
        Player = FindObjectOfType<PlayerMovement>().transform;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void Shoot()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        CanShoot = false;
        yield return new WaitForSeconds(_enemyData.ReloadTime);
        CanShoot = true;
    }
}
