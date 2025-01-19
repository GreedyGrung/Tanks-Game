using UnityEngine;

namespace TankGame.App.StaticData.Player
{
    [CreateAssetMenu(fileName = "PlayerHealthData", menuName = "Static Data/Player Data/Health Data")]
    public class PlayerHealthData : ScriptableObject
    {
        [SerializeField] private float _maxHealth;

        public float MaxHealth => _maxHealth;
    }
}