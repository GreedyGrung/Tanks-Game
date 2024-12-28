using System.Collections.Generic;
using TankGame.App.UI;
using TankGame.Core.Utils.Enums;

namespace TankGame.App.Infrastructure.Services.UI
{
    public class UIService : IUIService
    {
        private Dictionary<UIPanelId, UIPanelBase> _panels;

        public void ReceivePanels(Dictionary<UIPanelId, UIPanelBase> panels)
            => _panels = new(panels);

        public void Open(UIPanelId id)
            => _panels[id].gameObject.SetActive(true);
    }
}