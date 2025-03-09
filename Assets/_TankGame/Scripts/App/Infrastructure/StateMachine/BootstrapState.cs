﻿using _TankGame.App.Infrastructure.Services.AssetManagement;
using _TankGame.App.Infrastructure.Services.StaticData;
using _TankGame.App.Infrastructure.StateMachine.Interfaces;
using _TankGame.App.Utils;

namespace _TankGame.App.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;

            assetProvider.Initialize();
            staticData.LoadStaticData();
        }

        public void Enter() 
            => _sceneLoader.Load(SceneNames.Bootstrap, onLoaded: EnterLoadLevel);

        public void Exit()
        {

        }

        private void EnterLoadLevel()
            => _stateMachine.Enter<MainMenuState>();
    }
}