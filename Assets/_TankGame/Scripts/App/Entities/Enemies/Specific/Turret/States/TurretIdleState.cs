using TankGame.App.Entities.Enemies.Base.States;
using TankGame.App.Entities.Enemies.StateMachineScripts;

namespace TankGame.App.Entities.Enemies.Specific.Turret.States
{
    public class TurretIdleState : IdleState
    {
        private readonly Turret _turret;
        private bool _playerDetected;
        private bool _obstacleBetweenEnemyAndPlayer;

        public TurretIdleState(Turret turret, StateMachine stateMachine) : base(turret, stateMachine)
        {
            _turret = turret;
        }

        public override void DoChecks()
        {
            base.DoChecks();

            _playerDetected = _turret.PlayerDetected();
            _obstacleBetweenEnemyAndPlayer = _turret.AnyObstacleBetweenEnemyAndPlayer();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _turret.RotateTower();

            if (_playerDetected && !_obstacleBetweenEnemyAndPlayer)
            {
                _turret.StateMachine.ChangeState(_turret.AttackState);
            }
        }
    }
}