using System.Collections.Generic;
using _TankGame.App.UI;
using _TankGame.App.Utils.Enums;

namespace _TankGame.App.Infrastructure.Services.UI
{
    public interface IUIService
    {
        void Open(UIPanelId id);
        void ReceivePanels(Dictionary<UIPanelId, UIPanelBase> panels);
    }
}