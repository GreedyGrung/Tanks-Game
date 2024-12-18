using Assets.Scripts.Infrastructure;
using System.Collections.Generic;

namespace Assets.Scripts.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();
        Dictionary<UIPanelId, UIPanelBase> CreateUIPanels();
    }
}