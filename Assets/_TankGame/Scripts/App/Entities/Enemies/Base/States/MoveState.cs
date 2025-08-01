using TankGame.App.Entities.Enemies.StateMachineScripts;

namespace TankGame.App.Entities.Enemies.Base.States
{
    public class MoveState : State
    {
        public MoveState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
    }
}