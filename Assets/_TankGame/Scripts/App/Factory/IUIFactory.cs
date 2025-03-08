using System.Collections.Generic;
using System.Threading.Tasks;
using _TankGame.App.UI;
using _TankGame.App.Utils.Enums;

namespace _TankGame.App.Factory
{
    public interface IUIFactory
    {
        Task CreateUIRootAsync();
        Dictionary<UIPanelId, UIPanelBase> CreateUIPanels();
    }
}