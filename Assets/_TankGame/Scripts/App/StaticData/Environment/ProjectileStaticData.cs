using TankGame.Core.Utils.Enums;
using UnityEngine;

namespace TankGame.App.StaticData.Environment
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Static Data/Projectile")]
    public class ProjectileStaticData : ScriptableObject
    {
        [SerializeField] private ProjectileTypeId _projectileTypeId;
        [SerializeField, Min(1)] private float _moveSpeed = 10;
        [SerializeField, Min(1)] private float _damage;
        [SerializeField, Min(1)] private float _lifetime;

        public ProjectileTypeId Id => _projectileTypeId;
        public float MoveSpeed => _moveSpeed;
        public float Damage => _damage;
        public float Lifetime => _lifetime;
    }
}