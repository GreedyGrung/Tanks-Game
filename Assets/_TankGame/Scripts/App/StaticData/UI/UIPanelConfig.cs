using System;
using _TankGame.App.UI;
using _TankGame.App.Utils.Enums;

namespace _TankGame.App.StaticData.UI
{
    [Serializable]
    public class UIPanelConfig
    {
        public UIPanelId Id;
        public UIPanelBase Prefab;
    }
}
