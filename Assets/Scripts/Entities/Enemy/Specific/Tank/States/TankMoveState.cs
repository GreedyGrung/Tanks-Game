public class TankMoveState : MoveState
{
    private Tank _tank;
    private MoveStateData _data;
    private bool _playerDetected;
    private bool _obstacleBetweenEnemyAndPlayer;

    public TankMoveState(Tank tank, StateMachine stateMachine, MoveStateData data) : base(tank, stateMachine)
    {
        _tank = tank;
        _data = data;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _playerDetected = _tank.PlayerDetected();
        _obstacleBetweenEnemyAndPlayer = _tank.ObstacleBetweenEnemyAndPlayer();
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

        if (_playerDetected && !_obstacleBetweenEnemyAndPlayer)
        {
            _tank.StateMachine.ChangeState(_tank.AttackState);
        }
    }
}
