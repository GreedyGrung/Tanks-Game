using _TankGame.App.Entities.Enemies.StateMachineScripts;

namespace _TankGame.App.Entities.Enemies.Base.States
{
    public class DeadState : State
    {
        public DeadState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
    }
}