using TankGame.Runtime.Entities.Enemies.Base.States;
using TankGame.Runtime.Entities.Enemies.StateMachineScripts;

namespace TankGame.Runtime.Entities.Enemies.Specific.Tank.States
{
    public class TankAttackState : AttackState
    {
        private readonly Tank _tank;

        public TankAttackState(Tank tank, StateMachine stateMachine) : base(tank, stateMachine)
        {
            _tank = tank;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _tank.RotateTowerTowardsPlayer(_tank.Tower);

            if (!_tank.IsRotatingTower && _tank.CanShoot)
            {
                _tank.Shoot();
            }

            if (!PlayerDetected || ObstacleBetweenPlayerAndTurret)
            {
                StateMachine.ChangeState<TankMoveState>();
            }
        }
    }
}