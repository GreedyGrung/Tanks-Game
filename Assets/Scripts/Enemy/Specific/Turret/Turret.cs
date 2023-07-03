using UnityEngine;

public class Turret : Enemy
{
    public TurretIdleState IdleState { get; private set; }
    public TurretAttackState AttackState { get; private set; }
    public TurretDeadState DeadState { get; private set; }

    public bool IsRotating { get; private set; } = false;

    [SerializeField] private Transform _tower;
    [SerializeField] private float _rotationInterpolationFactor = 0.05f;

    private readonly float _rotationThreshold = 1f;

    public override void Start()
    {
        base.Start();

        ProjectilePool = FindObjectOfType<ArmorPiercingProjectilePool>();

        IdleState = new(this, StateMachine);
        AttackState = new(this, StateMachine);
        DeadState = new(this, StateMachine);

        StateMachine.Initialize(IdleState);
    }

    public void RotateTower()
    {
        Quaternion currentRotation = _tower.rotation;
        _tower.rotation = Quaternion.Euler(0f, 0f, currentRotation.eulerAngles.z + EnemyData.TowerRotationSpeed * Time.deltaTime);
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

    public void RotateTowerTowardsPlayer()
    {
        Vector2 direction = Player.position - _tower.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        _tower.rotation = Quaternion.Slerp(_tower.rotation, targetRotation, _rotationInterpolationFactor * EnemyData.TowerRotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(_tower.rotation, targetRotation) > _rotationThreshold)
        {
            IsRotating = true;
        }
        else
        {
            IsRotating = false;
        }
    }

    public bool ObstacleBetweenTurretAndPlayer()
    {
        Vector2 direction = Player.position - transform.position;
        float distance = Vector2.Distance(transform.position, Player.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, EnemyData.ObstacleLayer);

        return hit.collider != null;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log("Turret took damage: " + damage);
    }

    public override void Shoot()
    {
        base.Shoot();
        Projectile = ProjectilePool.Pool.TakeFromPool();
        Projectile.gameObject.layer = Constants.ENEMY_PROJECTILE_LAYER;
        Projectile.transform.position = BulletSpawn.position;
        Projectile.transform.rotation = BulletSpawn.rotation;
    }
}
