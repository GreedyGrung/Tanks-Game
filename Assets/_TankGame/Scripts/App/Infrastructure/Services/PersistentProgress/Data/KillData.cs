using System;
using System.Collections.Generic;

namespace TankGame.App.Infrastructure.Services.PersistentProgress.Data
{
    [Serializable]
    public class KillData
    {
        public List<string> ClearedSpawners = new();
    }
}