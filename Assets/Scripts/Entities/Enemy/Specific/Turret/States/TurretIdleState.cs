public class TurretIdleState : IdleState
{
    private Turret _turret;
    private bool _playerDetected;
    private bool _obstacleBetweenEnemyAndPlayer;

    public TurretIdleState(Turret turret, StateMachine stateMachine) : base(turret, stateMachine)
    {
        _turret = turret;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _playerDetected = _turret.PlayerDetected();
        _obstacleBetweenEnemyAndPlayer = _turret.ObstacleBetweenEnemyAndPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        DoChecks();
        _turret.RotateTower();

        if (_playerDetected && !_obstacleBetweenEnemyAndPlayer)
        {
            _turret.StateMachine.ChangeState(_turret.AttackState);
        }
    }
}
