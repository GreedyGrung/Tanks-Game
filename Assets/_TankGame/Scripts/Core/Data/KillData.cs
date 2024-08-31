using System;
using System.Collections.Generic;

namespace TankGame.Core.Scripts.Data
{
    [Serializable]
    public class KillData
    {
        public List<string> ClearedSpawners = new();
    }
}