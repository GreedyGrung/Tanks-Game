using UnityEngine;

public class TankAttackState : State
{
    private Tank _tank;
    private bool _playerDetected;
    private bool _obstacleBetweenPlayerAndTurret;

    public TankAttackState(Tank tank, StateMachine stateMachine) : base(tank, stateMachine)
    {
        _tank = tank;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _playerDetected = _tank.PlayerDetected();
        _obstacleBetweenPlayerAndTurret = _tank.ObstacleBetweenEnemyAndPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        DoChecks();
        _tank.RotateTowerTowardsPlayer(_tank.Tower);

        if (!_tank.IsRotatingTower && _tank.CanShoot)
        {
            _tank.Shoot();
        }

        if (!_playerDetected || _obstacleBetweenPlayerAndTurret)
        {
            _tank.StateMachine.ChangeState(_tank.MoveState);
        }
    }
}
