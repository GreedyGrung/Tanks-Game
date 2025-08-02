using TankGame.Runtime.Entities.Enemies.StateMachineScripts;

namespace TankGame.Runtime.Entities.Enemies.Base.States
{
    public class DeadState : State
    {
        public DeadState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
    }
}