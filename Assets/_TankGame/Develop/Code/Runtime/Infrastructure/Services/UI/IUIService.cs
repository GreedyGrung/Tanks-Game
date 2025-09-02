using System.Collections.Generic;
using TankGame.Runtime.UI;
using TankGame.Runtime.UI.Common;
using TankGame.Runtime.UI.Panels;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Infrastructure.Services.UI
{
    public interface IUIService
    {
        void Open(UIPanelId id);
        void Initialize(Dictionary<UIPanelId, UIPanelBase> panels, UIGenericHint hint);
        void Close(UIPanelId id);
        void ShowHint(string message, float duration = 3f);
    }
}