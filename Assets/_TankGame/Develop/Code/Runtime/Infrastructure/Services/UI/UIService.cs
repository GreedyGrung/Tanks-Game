using System.Collections.Generic;
using TankGame.Runtime.UI;
using TankGame.Runtime.UI.Panels;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Infrastructure.Services.UI
{
    public class UIService : IUIService
    {
        private Dictionary<UIPanelId, UIPanelBase> _panels;

        public void ReceivePanels(Dictionary<UIPanelId, UIPanelBase> panels) => 
            _panels = new(panels);

        public void Open(UIPanelId id) => 
            _panels[id].gameObject.SetActive(true);
        
        public void Close(UIPanelId id) => 
            _panels[id].gameObject.SetActive(false);
    }
}