using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.Runtime.UI;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Factory
{
    public interface IUIFactory
    {
        Task CreateUIRootAsync();
        Dictionary<UIPanelId, UIPanelBase> CreateUIPanels();
    }
}