using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttackState : State
{
    private Tank _tank;

    public TankAttackState(Tank tank, StateMachine stateMachine) : base(tank, stateMachine)
    {
        _tank = tank;
    }
}
