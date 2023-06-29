using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 90f;

    private PlayerInputHolder _playerInputHolder;
    private int _rotationInverseCoefficient;

    private void Start()
    {
        _playerInputHolder = GetComponent<PlayerInputHolder>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        transform.Translate(Vector3.up * _playerInputHolder.MovementInput.y * _moveSpeed * Time.deltaTime);
        _rotationInverseCoefficient = _playerInputHolder.MovementInput.y < 0 ? -1 : 1;
        transform.Rotate(Vector3.forward * -_playerInputHolder.MovementInput.x * _rotationInverseCoefficient * _rotationSpeed * Time.deltaTime);
    }
}
