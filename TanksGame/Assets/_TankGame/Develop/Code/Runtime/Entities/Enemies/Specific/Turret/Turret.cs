using TankGame.Runtime.Entities.Enemies.Base;
using TankGame.Runtime.Entities.Enemies.Specific.Turret.States;
using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Infrastructure.Services.PoolsService;
using TankGame.Runtime.Utils.Enums.Generated;
using UnityEngine;

namespace TankGame.Runtime.Entities.Enemies.Specific.Turret
{
    public class Turret : Enemy
    {
        [SerializeField] private Transform _tower;

        public Transform Tower => _tower;

        public override void Initialize(IPlayer player, IPoolsService poolsService)
        {
            base.Initialize(player, poolsService);

            StateMachine.RegisterState(new TurretIdleState(this, StateMachine));
            StateMachine.RegisterState(new TurretAttackState(this, StateMachine));

            StateMachine.Initialize<TurretIdleState>();

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