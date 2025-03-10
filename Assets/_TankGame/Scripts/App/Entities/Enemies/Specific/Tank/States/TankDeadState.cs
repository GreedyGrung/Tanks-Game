﻿using _TankGame.App.Entities.Enemies.Base.States;
using _TankGame.App.Entities.Enemies.StateMachineScripts;

namespace _TankGame.App.Entities.Enemies.Specific.Tank.States
{
    public class TankDeadState : DeadState
    {
        public TankDeadState(Tank tank, StateMachine stateMachine) : base(tank, stateMachine)
        {

        }
    }
}