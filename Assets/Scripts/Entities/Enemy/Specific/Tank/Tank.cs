using UnityEngine;

public class Tank : Enemy
{
    public TankMoveState MoveState { get; private set; }
    public TankAttackState AttackState { get; private set; }

    [SerializeField] private MoveStateData _moveStateData;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private Transform _tower;

    private Rigidbody2D _rigidbody;

    public Transform Tower => _tower;

    public override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody2D>();
        ProjectilePool = FindObjectOfType<ArmorPiercingProjectilePool>();

        MoveState = new(this, StateMachine, _moveStateData);
        AttackState = new(this, StateMachine);

        StateMachine.Initialize(MoveState);
    }

    public void Move()
    {
        Vector2 movement = transform.up * _moveStateData.MovementSpeed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    public bool CheckForWallCollision()
    {
        Vector2 raycastOrigin = _wallCheck.position;
        Vector2 raycastDirection = transform.up;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, EnemyData.WallCheckDistance, EnemyData.ObstacleLayer);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    public void RotateRandomly()
    {
        int direction = Random.Range(0, 2);
        float randomRotation = direction == 0 ? 90f : -90f;

        _rigidbody.MoveRotation(_rigidbody.rotation + randomRotation);
    }

    public override void Shoot()
    {
        base.Shoot();
        Projectile = ProjectilePool.Pool.TakeFromPool();
        Projectile.gameObject.layer = Constants.EnemyProjectileLayer;
        Projectile.transform.position = BulletSpawn.position;
        Projectile.transform.rotation = BulletSpawn.rotation;
    }

    private void OnDrawGizmos()
    {
        Vector2 raycastOrigin = _wallCheck.position;
        Vector2 raycastDirection = transform.up;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + raycastDirection * EnemyData.WallCheckDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, EnemyData.DetectionDistance);
    }
}
