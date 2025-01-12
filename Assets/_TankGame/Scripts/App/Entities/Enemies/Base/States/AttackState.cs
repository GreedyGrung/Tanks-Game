using TankGame.App.Entities.Enemies.StateMachineScripts;

namespace TankGame.App.Entities.Enemies.Base.States
{
    public class AttackState : State
    {
        protected bool PlayerDetected { get; private set; }
        protected bool ObstacleBetweenPlayerAndTurret { get; private set; }

        public AttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
            PlayerDetected = Enemy.PlayerDetected();
            ObstacleBetweenPlayerAndTurret = Enemy.AnyObstacleBetweenEnemyAndPlayer();
        }
    }
}