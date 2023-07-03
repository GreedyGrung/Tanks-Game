public class TankMoveState : State
{
    private Tank _tank;
    private MoveStateData _data;

    public TankMoveState(Tank tank, StateMachine stateMachine, MoveStateData data) : base(tank, stateMachine)
    {
        _tank = tank;
        _data = data;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _tank.Move();

        if (_tank.CheckForWallCollision())
        {
            _tank.RotateRandomly();
        }
    }
}
