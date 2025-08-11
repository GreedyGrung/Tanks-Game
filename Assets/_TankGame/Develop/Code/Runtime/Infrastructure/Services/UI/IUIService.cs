using System.Collections.Generic;
using TankGame.Runtime.UI;
using TankGame.Runtime.UI.Panels;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Infrastructure.Services.UI
{
    public interface IUIService
    {
        void Open(UIPanelId id);
        void ReceivePanels(Dictionary<UIPanelId, UIPanelBase> panels);
        void Close(UIPanelId id);
    }
}