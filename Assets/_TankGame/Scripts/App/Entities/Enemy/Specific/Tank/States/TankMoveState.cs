public class TankMoveState : MoveState
{
    private Tank _tank;
    private bool _playerDetected;
    private bool _obstacleBetweenEnemyAndPlayer;

    public TankMoveState(Tank tank, StateMachine stateMachine) : base(tank, stateMachine)
    {
        _tank = tank;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _playerDetected = _tank.PlayerDetected();
        _obstacleBetweenEnemyAndPlayer = _tank.ObstacleBetweenEnemyAndPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_playerDetected && !_obstacleBetweenEnemyAndPlayer)
        {
            _tank.StateMachine.ChangeState(_tank.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _tank.Move();

        if (_tank.CheckForWallCollision())
        {
            _tank.RotateRandomly();
        }
    }
}
