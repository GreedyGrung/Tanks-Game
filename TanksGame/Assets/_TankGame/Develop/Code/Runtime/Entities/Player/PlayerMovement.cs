using TankGame.Runtime.Infrastructure.Services.Input;
using TankGame.Runtime.StaticData.Player;
using UnityEngine;

namespace TankGame.Runtime.Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerMovementData _movementData;
        [SerializeField] private Transform _tankTower;

        private IInputService _inputService;
        private Camera _mainCamera;
        private Vector2 _mousePosition;
        private Quaternion _targetRotation;
        private int _bodyRotationInverseCoefficient;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void LogicUpdate()
        {
            if (_inputService == null)
            {
                return;
            }

            CalculateTowerRotationAngle();
            HandleTowerRotation();
        }

        public void PhysicsUpdate()
        {
            if (_inputService == null)
            {
                return;
            }

            HandleBodyMovement();
        }

        public void Init(IInputService inputService)
        {
            _inputService = inputService;
            _mainCamera = Camera.main;
        }

        private void HandleBodyMovement()
        {
            float movementSpeed =
            _inputService.MovementInput.x == 0 ?
            _movementData.MovementSpeed :
            _movementData.MovementSpeedWhenRotating;

            Vector2 movement = transform.up * _inputService.MovementInput.y * movementSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + movement);

            _bodyRotationInverseCoefficient = _inputService.MovementInput.y < 0 ? -1 : 1;
            float rotation = -_inputService.MovementInput.x * _bodyRotationInverseCoefficient * _movementData.BodyRotationSpeed * Time.fixedDeltaTime;
            float newRotation = _rigidbody.rotation + rotation;

            _rigidbody.MoveRotation(newRotation);
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
}