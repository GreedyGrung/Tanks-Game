using TankGame.Runtime.Infrastructure.Services.SavingLoading;
using TankGame.Runtime.Infrastructure.Services.UI;
using Zenject;

namespace TankGame.Runtime.UI.Buttons
{
    public class UIButtonSaveGame : UIButtonBehaviourBase
    {
        private ISaveLoadService _saveLoadService;
        private IUIService _uiService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService, IUIService uiService)
        {
            _saveLoadService = saveLoadService;
            _uiService = uiService;
        }

        protected override void HandleClick()
        {
            _uiService.ShowHint("The game has been saved!");
            _saveLoadService.SaveProgress();
        }
    }
}