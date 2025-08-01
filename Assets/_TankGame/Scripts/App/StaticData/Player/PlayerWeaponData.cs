using UnityEngine;

namespace TankGame.App.StaticData.Player
{
    [CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "Static Data/Player Data/Weapon Data")]
    public class PlayerWeaponData : ScriptableObject
    {
        [SerializeField] private float _reloadTime;

        public float ReloadTime => _reloadTime;
    }
}