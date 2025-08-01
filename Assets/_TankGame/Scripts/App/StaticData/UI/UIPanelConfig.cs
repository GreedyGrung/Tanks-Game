using System;
using TankGame.App.UI;
using TankGame.App.Utils.Enums;

namespace TankGame.App.StaticData.UI
{
    [Serializable]
    public class UIPanelConfig
    {
        public UIPanelId Id;
        public UIPanelBase Prefab;
    }
}
