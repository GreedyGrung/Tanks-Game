using TankGame.Runtime.Infrastructure.Services.SavingLoading;
using Zenject;

namespace TankGame.Runtime.UI.Buttons
{
    public class UIButtonSaveGame : UIButtonBehaviourBase
    {
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        protected override void HandleClick()
        {
            _saveLoadService.SaveProgress();
        }
    }
}