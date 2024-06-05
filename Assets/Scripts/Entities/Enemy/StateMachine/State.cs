public class State
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
