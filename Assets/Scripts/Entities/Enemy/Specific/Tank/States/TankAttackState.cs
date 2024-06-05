public class TankAttackState : AttackState
{
    private readonly Tank _tank;

    public TankAttackState(Tank tank, StateMachine stateMachine) : base(tank, stateMachine)
    {
        _tank = tank;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _tank.RotateTowerTowardsPlayer(_tank.Tower);

        if (!_tank.IsRotatingTower && _tank.CanShoot)
        {
            _tank.Shoot();
        }

        if (!PlayerDetected || ObstacleBetweenPlayerAndTurret)
        {
            _tank.StateMachine.ChangeState(_tank.MoveState);
        }
    }
}
