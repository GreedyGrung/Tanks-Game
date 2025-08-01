using System.Collections.Generic;
using TankGame.App.UI;
using TankGame.App.Utils.Enums;

namespace TankGame.App.Infrastructure.Services.UI
{
    public interface IUIService
    {
        void Open(UIPanelId id);
        void ReceivePanels(Dictionary<UIPanelId, UIPanelBase> panels);
    }
}