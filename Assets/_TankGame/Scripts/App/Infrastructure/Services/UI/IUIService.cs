using System.Collections.Generic;
using TankGame.App.UI;
using TankGame.Core.Services;
using TankGame.Core.Utils.Enums;

namespace TankGame.App.Infrastructure.Services.UI
{
    public interface IUIService : IService
    {
        void Open(UIPanelId id);
        void ReceivePanels(Dictionary<UIPanelId, UIPanelBase> panels);
    }
}