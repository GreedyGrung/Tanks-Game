using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TankGame.Runtime.UI.Common;
using TankGame.Runtime.UI.Panels;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.Factory
{
    public interface IUIFactory
    {
        UniTask CreateUIRootAsync();
        Dictionary<UIPanelId, UIPanelBase> CreateUIPanels();
        UniTask CreateHintsRootAsync();
        UniTask<UIGenericHint> CreateGenericHint();
    }
}