using System;
using System.Collections.Generic;

namespace _TankGame.App.Infrastructure.Services.PersistentProgress.Data
{
    [Serializable]
    public class KillData
    {
        public List<string> ClearedSpawners = new();
    }
}