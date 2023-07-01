using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIdleState : State
{
    private Turret _turret;
    private bool _playerDetected;
    private bool _obstacleBetweenPlayerAndTurret;

    public TurretIdleState(Turret turret, StateMachine stateMachine) : base(turret, stateMachine)
    {
        _turret = turret;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _playerDetected = _turret.PlayerDetected();
        _obstacleBetweenPlayerAndTurret = _turret.ObstacleBetweenTurretAndPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        DoChecks();
        _turret.RotateTower();

        if (_playerDetected && !_obstacleBetweenPlayerAndTurret)
        {
            _turret.StateMachine.ChangeState(_turret.AttackState);
        }
    }
}
