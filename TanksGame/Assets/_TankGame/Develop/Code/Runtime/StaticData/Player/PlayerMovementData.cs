using UnityEngine;

namespace TankGame.Runtime.StaticData.Player
{
    [CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Static Data/Player Data/Movement Data")]
    public class PlayerMovementData : ScriptableObject
    {
        [SerializeField, Min(0)] private float _movementSpeed = 2f;
        [SerializeField, Min(0)] private float _movementSpeedWhenRotating = 4f;
        [SerializeField, Min(0)] private float _bodyRotationSpeed = 90f;
        [SerializeField, Min(0)] private float _towerRotationSpeed = 150f;

        public float MovementSpeed => _movementSpeed;
        public float MovementSpeedWhenRotating => _movementSpeedWhenRotating;
        public float BodyRotationSpeed => _bodyRotationSpeed;
        public float TowerRotationSpeed => _towerRotationSpeed;
    }
}