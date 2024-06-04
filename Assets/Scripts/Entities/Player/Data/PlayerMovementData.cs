using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Data/Player Data/Movement Data")]
public class PlayerMovementData : ScriptableObject
{
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _bodyRotationSpeed = 90f;
    [SerializeField] private float _towerRotationSpeed = 150f;

    public float MovementSpeed => _movementSpeed;
    public float BodyRotationSpeed => _bodyRotationSpeed;
    public float TowerRotationSpeed => _towerRotationSpeed;
}
