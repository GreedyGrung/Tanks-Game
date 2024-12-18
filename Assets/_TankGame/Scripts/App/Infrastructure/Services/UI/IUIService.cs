using Assets.Scripts.Infrastructure;
using System.Collections.Generic;

public interface IUIService : IService
{
    void Open(UIPanelId id);
    void ReceivePanels(Dictionary<UIPanelId, UIPanelBase> panels);
}