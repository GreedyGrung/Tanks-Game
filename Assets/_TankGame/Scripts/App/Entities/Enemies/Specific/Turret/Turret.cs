using TankGame.App.Entities.Enemies.Base;
using TankGame.App.Entities.Enemies.Specific.Turret.States;
using TankGame.App.Entities.Interfaces;
using TankGame.App.Object_Pool;
using TankGame.Core.Utils.Enums.Generated;
using UnityEngine;

namespace TankGame.App.Entities.Enemies.Specific.Turret
{
    public class Turret : Enemy
    {
        public TurretIdleState IdleState { get; private set; }
        public TurretAttackState AttackState { get; private set; }

        public bool IsRotating { get; private set; } = false;

        [SerializeField] private Transform _tower;

        public Transform Tower => _tower;

        public override void Awake()
        {
            base.Awake();

            ProjectilePool = FindObjectOfType<HighExplosiveProjectilePool>();
        }

        public override void Init(IPlayer player)
        {
            base.Init(player);

            IdleState = new(this, StateMachine);
            AttackState = new(this, StateMachine);

            StateMachine.Initialize(IdleState);

            SetIsInit();
        }

        public void RotateTower()
        {
            Quaternion currentRotation = _tower.rotation;
            _tower.rotation = Quaternion.Euler(0f, 0f, currentRotation.eulerAngles.z + EnemyData.TowerRotationSpeed * Time.deltaTime);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
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
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, EnemyData.DetectionDistance);
        }
    }
}