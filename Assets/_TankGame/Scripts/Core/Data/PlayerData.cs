using System;

namespace Assets.Scripts.Data
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
