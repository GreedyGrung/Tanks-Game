using System.Collections;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    public event Action OnKilled;

    [SerializeField] private EnemyTypeId _enemyTypeId;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private EnemyHealthBar _healthBar;
    [SerializeField] private float _rotationInterpolationFactor = 0.05f;

    private EnemyVisuals _enemyVisuals;
    private readonly float _rotationThreshold = 10f;
    private bool _isExploding = false;
    private bool _isInit;

    public StateMachine StateMachine { get; private set; }
    public Transform Player { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public BaseProjectilePool ProjectilePool { get; protected set; }
    public Projectile Projectile { get; protected set; }
    public IHealth Health { get; private set; }

    public BaseEnemyStaticData EnemyData { get; private set; }
    public Transform BulletSpawn => _bulletSpawn;

    public bool CanShoot { get; private set; } = true;
    public bool IsRotatingTower { get; private set; }

    public virtual void Awake()
    {
        StateMachine = new();
        Rigidbody = GetComponent<Rigidbody2D>();
        _enemyVisuals = GetComponent<EnemyVisuals>();
    }

    public virtual void Update()
    {
        if (_isExploding || _isInit == false)
        {
            return;
        }

        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        if (_isExploding || _isInit == false)
        {
            return;
        }

        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void Init(IPlayer player)
    {
        Player = player.Transform;
        Health = new EnemyHealth(EnemyData.MaxHealth);
        _healthBar.Construct(this);
    }

    protected void SetIsInit()
    {
        _isInit = true;
    }

    public void SetData(BaseEnemyStaticData data)
    {
        EnemyData = data;
    }

    public bool ObstacleBetweenEnemyAndPlayer()
    {
        if (Player == null)
        {
            return false;
        }

        Vector2 direction = Player.position - transform.position;
        float distance = Vector2.Distance(transform.position, Player.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, EnemyData.DetectionObstacleLayer);

        return hit.collider != null;
    }

    public bool PlayerDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, EnemyData.DetectionDistance, EnemyData.PlayerLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.transform == Player)
            {
                return true;
            }
        }

        return false;
    }

    public void RotateTowerTowardsPlayer(Transform tower)
    {
        Vector2 direction = Player.position - tower.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        tower.rotation = Quaternion.Slerp(tower.rotation, targetRotation, _rotationInterpolationFactor * EnemyData.TowerRotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(tower.rotation, targetRotation) > _rotationThreshold)
        {
            IsRotatingTower = true;
        }
        else
        {
            IsRotatingTower = false;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        Health.Subtract(damage);

        if (Health.IsDead)
        {
            _isExploding = true;
            _enemyVisuals.PlayExplosionAnimation();
            OnKilled?.Invoke();
        }
    }

    public virtual void Shoot()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        CanShoot = false;
        yield return new WaitForSeconds(EnemyData.ReloadTime);
        CanShoot = true;
    }
}
