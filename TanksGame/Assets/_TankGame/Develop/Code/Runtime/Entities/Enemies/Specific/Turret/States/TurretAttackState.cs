using TankGame.Runtime.Entities.Enemies.Base.States;
using TankGame.Runtime.Entities.Enemies.StateMachineScripts;

namespace TankGame.Runtime.Entities.Enemies.Specific.Turret.States
{
    public class TurretAttackState : AttackState
    {
        private readonly Turret _turret;

        public TurretAttackState(Turret turret, StateMachine stateMachine) : base(turret, stateMachine)
        {
            _turret = turret;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _turret.RotateTowerTowardsPlayer(_turret.Tower);

            if (!_turret.IsRotatingTower && _turret.CanShoot)
            {
                _turret.Shoot();
            }

            if (!PlayerDetected || ObstacleBetweenPlayerAndTurret)
            {
                StateMachine.ChangeState<TurretIdleState>();
            }
        }
    }
}