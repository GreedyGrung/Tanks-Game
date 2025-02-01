﻿using TankGame.App.Infrastructure.StateMachine;
using TankGame.App.Infrastructure.StateMachine.Interfaces;
using Zenject;

namespace TankGame.App.UI.Buttons
{
    public class UIButtonPlay : UIButtonBehaviourBase
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine stateMachine)
        {
            _gameStateMachine = stateMachine;
        }

        protected override void HandleClick()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}
