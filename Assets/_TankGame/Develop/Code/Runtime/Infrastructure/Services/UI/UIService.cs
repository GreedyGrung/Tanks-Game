using System.Collections.Generic;
using TankGame.Runtime.UI.Common;
using TankGame.Runtime.UI.Panels;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Infrastructure.Services.UI
{
    public class UIService : IUIService
    {
        private Dictionary<UIPanelId, UIPanelBase> _panels;
        private UIGenericHint _hint;

        public void Initialize(Dictionary<UIPanelId, UIPanelBase> panels, UIGenericHint hint)
        {
            _panels = new(panels);
            _hint = hint;
        }

        public void Open(UIPanelId id) => _panels[id].gameObject.SetActive(true);
        
        public void Close(UIPanelId id) => _panels[id].gameObject.SetActive(false);

        public void ShowHint(string message, float duration = 3f) => _hint.Show(message, duration);
    }
}