using _TankGame.App.Entities.Enemies.StateMachineScripts;

namespace _TankGame.App.Entities.Enemies.Base.States
{
    public class IdleState : State
    {
        public IdleState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
    }
}