using _TankGame.App.Entities.Enemies.Base;
using _TankGame.App.Entities.Enemies.Specific.Turret.States;
using _TankGame.App.Entities.Interfaces;
using _TankGame.App.Infrastructure.Services.PoolsService;
using TankGame.Core.Utils.Enums.Generated;
using UnityEngine;

namespace _TankGame.App.Entities.Enemies.Specific.Turret
{
    public class Turret : Enemy
    {
        public TurretIdleState IdleState { get; private set; }
        public TurretAttackState AttackState { get; private set; }

        public bool IsRotating { get; private set; } = false;

        [SerializeField] private Transform _tower;

        public Transform Tower => _tower;

        public override void Initialize(IPlayer player, IPoolsService poolsService)
        {
            base.Initialize(player, poolsService);

            IdleState = new(this, StateMachine);
            AttackState = new(this, StateMachine);

            StateMachine.Initialize(IdleState);

            SetIsInitialized();
        }

        public void RotateTower()
        {
            Quaternion currentRotation = _tower.rotation;
            _tower.rotation = Quaternion.Euler(0f, 0f, currentRotation.eulerAngles.z + EnemyData.TowerRotationSpeed * Time.deltaTime);
        }

        public override void Shoot()
        {
            base.Shoot();

            Projectile = PoolsService.GetProjectile(EnemyData.ProjectileType);
            Projectile.gameObject.layer = (int)Layers.EnemyProjectile;
            Projectile.transform.SetPositionAndRotation(BulletSpawn.position, BulletSpawn.rotation);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, EnemyData.DetectionDistance);
        }
    }
}