using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Infrastructure.Services.Input;
using TankGame.Runtime.Infrastructure.Services.Pause;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;

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
            _player = payload.Player;
            _pauseService.Register(_player as IPausable);

            _inputService.OnPausePressed += TogglePause;
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
    }
}