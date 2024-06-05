using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementData _movementData;
    [SerializeField] private Transform _tankTower;

    private IInputService _inputService;
    private Camera _mainCamera;
    private Vector2 _mousePosition;
    Quaternion _targetRotation;
    private int _bodyRotationInverseCoefficient;

    public void Init(IInputService inputService)
    {
        _inputService = inputService;
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
        float movementSpeed = 
            _inputService.MovementInput.x == 0 ? 
            _movementData.MovementSpeed : 
            _movementData.MovementSpeedWhenRotating;

        transform.Translate(Vector3.up * _inputService.MovementInput.y * movementSpeed * Time.deltaTime);
        _bodyRotationInverseCoefficient = _inputService.MovementInput.y < 0 ? -1 : 1;
        transform.Rotate(Vector3.forward * -_inputService.MovementInput.x * _bodyRotationInverseCoefficient * _movementData.BodyRotationSpeed * Time.deltaTime);
    }

    private void CalculateTowerRotationAngle()
    {
        _mousePosition = _mainCamera.ScreenToWorldPoint(_inputService.MousePosition);
        _targetRotation = Quaternion.LookRotation(Vector3.forward, _mousePosition - (Vector2)_tankTower.position);
    }

    private void HandleTowerRotation()
    {
        float rotationStep = _movementData.TowerRotationSpeed * Time.deltaTime;
        _tankTower.transform.rotation = Quaternion.RotateTowards(_tankTower.transform.rotation, _targetRotation, rotationStep);
    }
}
