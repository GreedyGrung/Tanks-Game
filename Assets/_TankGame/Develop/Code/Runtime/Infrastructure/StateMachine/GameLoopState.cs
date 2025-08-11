using System.Collections.Generic;
using TankGame.Runtime.Entities.Enemies.Base;
using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Entities.Player;
using TankGame.Runtime.Infrastructure.Services.Input;
using TankGame.Runtime.Infrastructure.Services.Pause;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using UnityEngine;
using Zenject;

namespace TankGame.Runtime.Infrastructure.StateMachine
{
    public class GameLoopState : IPayloadedState<GameLoopPayload>
    {
        private readonly IPauseService _pauseService;
        private readonly IInputService _inputService;
        private IPlayer _player;

        public GameLoopState(IPauseService pauseService, IInputService inputService)
        {
            _pauseService = pauseService;
            _inputService = inputService;
        }
        
        public void Enter(GameLoopPayload payload)
        {
            _inputService.OnPausePressed += TogglePause;

            _player = payload.Player;
            _pauseService.Register(_player as IPausable);
            
            Debug.LogError("player " + _player.Health.Value);
        }

        public void Exit()
        {
            _inputService.OnPausePressed -= TogglePause;
            _pauseService.Dispose();
        }
        
        private void TogglePause() => _pauseService.TogglePause();
    }

    public class GameLoopPayload
    {
        public IPlayer Player;
        public List<Enemy> Enemies;
    }
}