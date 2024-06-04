using UnityEngine;

[CreateAssetMenu(fileName = "MoveStateData", menuName = "Data/Enemy Data/Move State Data")]
public class MoveStateData : ScriptableObject
{
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 90f;

    public float MovementSpeed => _movementSpeed;
    public float RotationSpeed => _rotationSpeed;
}
