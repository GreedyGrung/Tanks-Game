using TankGame.Runtime.Entities.Enemies.StateMachineScripts;

namespace TankGame.Runtime.Entities.Enemies.Base.States
{
    public class MoveState : State
    {
        public MoveState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
    }
}