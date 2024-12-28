using TankGame.App.Entities.Enemies.Base;
using TankGame.App.Entities.Enemies.Base.Data;
using TankGame.App.Entities.Enemies.Specific.Tank.States;
using TankGame.App.Entities.Interfaces;
using TankGame.App.Object_Pool;
using TankGame.Core.Utils.Enums.Generated;
using UnityEngine;

namespace TankGame.App.Entities.Enemies.Specific.Tank
{
    public class Tank : Enemy
    {
        [SerializeField] private Transform _wallCheck;
        [SerializeField] private Transform _tower;

        private Rigidbody2D _rigidbody;
        private MovingEnemyStaticData _movingEnemyData;

        public Transform Tower => _tower;

        public TankMoveState MoveState { get; private set; }
        public TankAttackState AttackState { get; private set; }

        public override void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody2D>();
            ProjectilePool = FindObjectOfType<ArmorPiercingProjectilePool>();
        }

        public override void Init(IPlayer player)
        {
            base.Init(player);

            _movingEnemyData = EnemyData as MovingEnemyStaticData;

            MoveState = new(this, StateMachine);
            AttackState = new(this, StateMachine);

            StateMachine.Initialize(MoveState);

            SetIsInit();
        }

        public void Move()
        {
            Vector2 movement = transform.up * _movingEnemyData.MovementSpeed * Time.deltaTime;
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
            Projectile.gameObject.layer = (int)Layers.EnemyProjectile;
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
}