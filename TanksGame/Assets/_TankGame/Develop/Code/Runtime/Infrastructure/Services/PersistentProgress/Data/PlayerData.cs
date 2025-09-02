using System;

namespace TankGame.Runtime.Infrastructure.Services.PersistentProgress.Data
{
    [Serializable]
    public class PlayerData
    {
        public float Health;
        public PositionOnLevel PositionOnLevel;

        public PlayerData(string initialLevel) 
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}
