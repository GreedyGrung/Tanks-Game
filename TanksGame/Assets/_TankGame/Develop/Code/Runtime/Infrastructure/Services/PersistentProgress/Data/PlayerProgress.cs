using System;

namespace TankGame.Runtime.Infrastructure.Services.PersistentProgress.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public KillData KillData;
        public PlayerData PlayerData;

        public PlayerProgress(string initialLevel) 
        {
            KillData = new KillData();
            PlayerData = new PlayerData(initialLevel);
        }
    }
}
