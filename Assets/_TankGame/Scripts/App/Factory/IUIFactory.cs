using Assets.Scripts.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.Factory
{
    public interface IUIFactory : IService
    {
        Task CreateUIRootAsync();
        Dictionary<UIPanelId, UIPanelBase> CreateUIPanels();
    }
}