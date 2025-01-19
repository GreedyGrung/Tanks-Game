using TankGame.App.Entities.Enemies.StateMachineScripts;

namespace TankGame.App.Entities.Enemies.Base.States
{
    public class DeadState : State
    {
        public DeadState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
    }
}