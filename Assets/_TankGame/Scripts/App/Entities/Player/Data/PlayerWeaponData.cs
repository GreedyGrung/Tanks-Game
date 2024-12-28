using UnityEngine;

namespace TankGame.App.Entities.Player.Data
{
    [CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "Data/Player Data/Weapon Data")]
    public class PlayerWeaponData : ScriptableObject
    {
        [SerializeField] private float _reloadTime;

        public float ReloadTime => _reloadTime;
    }
}