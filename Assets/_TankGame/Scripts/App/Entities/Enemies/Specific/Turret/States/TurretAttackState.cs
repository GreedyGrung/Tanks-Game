using _TankGame.App.Entities.Enemies.Base.States;
using _TankGame.App.Entities.Enemies.StateMachineScripts;

namespace _TankGame.App.Entities.Enemies.Specific.Turret.States
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
                _turret.StateMachine.ChangeState(_turret.IdleState);
            }
        }
    }
}