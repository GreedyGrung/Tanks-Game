public interface IState : IBaseState
{
    void Enter();
}

public interface IPayloadedState<TPayload> : IBaseState
{
    void Enter(TPayload payload);
}

public interface IBaseState
{
    void Exit();
}