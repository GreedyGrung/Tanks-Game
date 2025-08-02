using TankGame.Runtime.Entities.Enemies.StateMachineScripts;

namespace TankGame.Runtime.Entities.Enemies.Base.States
{
    public class IdleState : State
    {
        public IdleState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
    }
}