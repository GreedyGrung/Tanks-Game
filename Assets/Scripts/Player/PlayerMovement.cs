using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Tank Body")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _bodyRotationSpeed = 90f;

    [Header("Tank Tower")]
    [SerializeField] private Transform _tankTower;
    [SerializeField] private float _towerRotationSpeed = 90f;

    private PlayerInputHolder _playerInputHolder;
    private Camera _mainCamera;
    private Vector2 _mousePosition;
    Quaternion _targetRotation;
    private int _bodyRotationInverseCoefficient;

    private void Start()
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
        transform.Translate(Vector3.up * _playerInputHolder.MovementInput.y * _moveSpeed * Time.deltaTime);
        _bodyRotationInverseCoefficient = _playerInputHolder.MovementInput.y < 0 ? -1 : 1;
        transform.Rotate(Vector3.forward * -_playerInputHolder.MovementInput.x * _bodyRotationInverseCoefficient * _bodyRotationSpeed * Time.deltaTime);
    }

    private void CalculateTowerRotationAngle()
    {
        _mousePosition = _mainCamera.ScreenToWorldPoint(_playerInputHolder.MousePosition);
        _targetRotation = Quaternion.LookRotation(Vector3.forward, _mousePosition - (Vector2)_tankTower.position);
    }

    private void HandleTowerRotation()
    {
        float rotationStep = _towerRotationSpeed * Time.deltaTime;
        _tankTower.transform.rotation = Quaternion.RotateTowards(_tankTower.transform.rotation, _targetRotation, rotationStep);
    }
}
