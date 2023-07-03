using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementData _movementData;
    [SerializeField] private Transform _tankTower;

    private PlayerInputHolder _playerInputHolder;
    private Camera _mainCamera;
    private Vector2 _mousePosition;
    Quaternion _targetRotation;
    private int _bodyRotationInverseCoefficient;

    public void Init()
    {
        _playerInputHolder = GetComponent<PlayerInputHolder>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        CalculateTowerRotationAngle();
    }

    private void FixedUpdate()
    {
        HandleBodyMovement();
        HandleTowerRotation();
    }

    private void HandleBodyMovement()
    {
        transform.Translate(Vector3.up * _playerInputHolder.MovementInput.y * _movementData.MovementSpeed * Time.deltaTime);
        _bodyRotationInverseCoefficient = _playerInputHolder.MovementInput.y < 0 ? -1 : 1;
        transform.Rotate(Vector3.forward * -_playerInputHolder.MovementInput.x * _bodyRotationInverseCoefficient * _movementData.BodyRotationSpeed * Time.deltaTime);
    }

    private void CalculateTowerRotationAngle()
    {
        _mousePosition = _mainCamera.ScreenToWorldPoint(_playerInputHolder.MousePosition);
        _targetRotation = Quaternion.LookRotation(Vector3.forward, _mousePosition - (Vector2)_tankTower.position);
    }

    private void HandleTowerRotation()
    {
        float rotationStep = _movementData.TowerRotationSpeed * Time.deltaTime;
        _tankTower.transform.rotation = Quaternion.RotateTowards(_tankTower.transform.rotation, _targetRotation, rotationStep);
    }
}
