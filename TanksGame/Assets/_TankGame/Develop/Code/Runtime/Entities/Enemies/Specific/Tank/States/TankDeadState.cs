using TankGame.Runtime.Entities.Enemies.Base.States;
using TankGame.Runtime.Entities.Enemies.StateMachineScripts;

namespace TankGame.Runtime.Entities.Enemies.Specific.Tank.States
{
    public class TankDeadState : DeadState
    {
        public TankDeadState(Tank tank, StateMachine stateMachine) : base(tank, stateMachine)
        {

        }
    }
}