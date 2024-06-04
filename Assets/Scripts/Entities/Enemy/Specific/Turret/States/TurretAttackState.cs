public class TurretAttackState : State
{
    private Turret _turret;
    private bool _playerDetected;
    private bool _obstacleBetweenPlayerAndTurret;

    public TurretAttackState(Turret turret, StateMachine stateMachine) : base(turret, stateMachine)
    {
        _turret = turret;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _playerDetected = _turret.PlayerDetected();
        _obstacleBetweenPlayerAndTurret = _turret.ObstacleBetweenEnemyAndPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        DoChecks();

        _turret.RotateTowerTowardsPlayer(_turret.Tower);

        if (!_turret.IsRotatingTower && _turret.CanShoot)
        {
            _turret.Shoot();
        }

        if (!_playerDetected || _obstacleBetweenPlayerAndTurret)
        {
            _turret.StateMachine.ChangeState(_turret.IdleState);
        }
    }
}
