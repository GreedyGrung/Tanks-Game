using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDeadState : State
{
    public TurretDeadState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
}
