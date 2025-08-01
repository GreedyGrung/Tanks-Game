using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.App.UI;
using TankGame.App.Utils.Enums;

namespace TankGame.App.Factory
{
    public interface IUIFactory
    {
        Task CreateUIRootAsync();
        Dictionary<UIPanelId, UIPanelBase> CreateUIPanels();
    }
}