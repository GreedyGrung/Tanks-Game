using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.App.UI;
using TankGame.Core.Services;
using TankGame.Core.Utils.Enums;

namespace TankGame.App.Factory
{
    public interface IUIFactory : IService
    {
        Task CreateUIRootAsync();
        Dictionary<UIPanelId, UIPanelBase> CreateUIPanels();
    }
}