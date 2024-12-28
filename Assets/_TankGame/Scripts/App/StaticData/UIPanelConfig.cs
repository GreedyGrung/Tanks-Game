using System;
using TankGame.App.UI;
using TankGame.Core.Utils.Enums;

namespace TankGame.App.StaticData
{
    [Serializable]
    public class UIPanelConfig
    {
        public UIPanelId Id;
        public UIPanelBase Prefab;
    }
}
