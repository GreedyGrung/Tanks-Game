using UnityEngine;

namespace TankGame.Runtime.StaticData.Enemies
{
    [CreateAssetMenu(fileName = "MovingEnemyStaticData", menuName = "Static Data/Enemy Data/Moving Enemy Static Data")]
    public class MovingEnemyStaticData : BaseEnemyStaticData
    {
        [SerializeField] private float _movementSpeed = 2f;
        [SerializeField] private float _rotationSpeed = 90f;

        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
    }
}