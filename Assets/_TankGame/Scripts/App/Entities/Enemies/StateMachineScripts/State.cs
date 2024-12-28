using TankGame.App.Entities.Enemies.Base;

namespace TankGame.App.Entities.Enemies.StateMachineScripts
{
    public abstract class State
    {
        protected StateMachine StateMachine { get; private set; }
        protected Enemy Enemy { get; private set; }

        public State(Enemy enemy, StateMachine stateMachine)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            DoChecks();
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }

        public virtual void Exit()
        {

        }

        public virtual void DoChecks()
        {

        }
    }
}