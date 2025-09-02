using TankGame.Runtime.Infrastructure.Services.Pause;
using Zenject;

namespace TankGame.Runtime.UI.Panels
{
    public class UIPausePanel : UIPanelBase
    {
        private IPauseService _pauseService;

        [Inject]
        private void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }

        protected override void Close()
        {
            base.Close();
            
            _pauseService.TogglePause();
        }
    }
}