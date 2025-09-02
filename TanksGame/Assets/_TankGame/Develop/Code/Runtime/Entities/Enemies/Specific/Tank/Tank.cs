using TankGame.Runtime.Entities.Enemies.Base;
using TankGame.Runtime.Entities.Enemies.Specific.Tank.States;
using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Infrastructure.Services.PoolsService;
using TankGame.Runtime.StaticData.Enemies;
using TankGame.Runtime.Utils.Enums.Generated;
using UnityEngine;

namespace TankGame.Runtime.Entities.Enemies.Specific.Tank
{
    public class Tank : Enemy
    {
        [SerializeField] private Transform _wallCheck;
        [SerializeField] private Transform _tower;

        private MovingEnemyStaticData _movingEnemyData;

        public Transform Tower => _tower;

        public override void Initialize(IPlayer player, IPoolsService poolsService)
        {
            base.Initialize(player, poolsService);

            _movingEnemyData = EnemyData as MovingEnemyStaticData;

            StateMachine.RegisterState(new TankMoveState(this, StateMachine));
            StateMachine.RegisterState(new TankAttackState(this, StateMachine));
            StateMachine.RegisterState(new TankDeadState(this, StateMachine));

            StateMachine.Initialize<TankMoveState>();

            SetIsInitialized();
        }

        public void Move()
        {
            Vector2 movement = transform.up * _movingEnemyData.MovementSpeed * Time.deltaTime;
            Rigidbody.MovePosition(Rigidbody.position + movement);
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

            Rigidbody.MoveRotation(Rigidbody.rotation + randomRotation);
        }

        public override void Shoot()
        {
            base.Shoot();

            Projectile = PoolsService.GetProjectile(_movingEnemyData.ProjectileType);
            Projectile.gameObject.layer = (int)Layers.EnemyProjectile;
            Projectile.transform.SetPositionAndRotation(BulletSpawn.position, BulletSpawn.rotation);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (Health.IsDead)
            {
                StateMachine.ChangeState<TankDeadState>();
            }
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