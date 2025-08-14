using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.Runtime.UI;
using TankGame.Runtime.UI.Common;
using TankGame.Runtime.UI.Panels;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Factory
{
    public interface IUIFactory
    {
        Task CreateUIRootAsync();
        Dictionary<UIPanelId, UIPanelBase> CreateUIPanels();
        Task CreateHintsRootAsync();
        Task<GenericHint> CreateGenericHint();
    }
}