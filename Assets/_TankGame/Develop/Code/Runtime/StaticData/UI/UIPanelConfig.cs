using System;
using TankGame.Runtime.UI;
using TankGame.Runtime.Utils.Enums;

namespace TankGame.Runtime.StaticData.UI
{
    [Serializable]
    public class UIPanelConfig
    {
        public UIPanelId Id;
        public UIPanelBase Prefab;
    }
}
