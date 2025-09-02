using System;
using App.Infrastructure.Services.Input;
using GreedyLogger;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TankGame.Runtime.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private readonly PlayerControls _playerControls;

        public InputService()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            Subscribe();
            
            GLogger.Log("Input service initialized!", LogContext.Infrastructure);
        }

        public event Action OnPausePressed;
        public Vector2 MovementInput { get; private set; }
        public Vector2 MousePosition { get; private set; }

        public event Action OnAttackPressed;
        public event Action OnFirstProjectileTypeSelected;
        public event Action OnSecondProjectileTypeSelected;

        private void Subscribe()
        {
            _playerControls.Player.Movement.performed += Move;
            _playerControls.Player.Movement.canceled += Move;
            _playerControls.Player.MousePosition.performed += Look;
            _playerControls.Player.MouseLeftButtonClick.performed += Attack;
            _playerControls.Player.ChooseFirstProjectileType.performed += ChooseFirstProjectileType;
            _playerControls.Player.ChooseSecondProjectileType.performed += ChooseSecondProjectileType;
            _playerControls.Player.Pause.performed += Pause;
        }

        private void Unsubscribe()
        {
            _playerControls.Player.Movement.performed -= Move;
            _playerControls.Player.Movement.canceled -= Move;
            _playerControls.Player.MousePosition.performed -= Look;
            _playerControls.Player.MouseLeftButtonClick.performed -= Attack;
            _playerControls.Player.ChooseFirstProjectileType.performed -= ChooseFirstProjectileType;
            _playerControls.Player.ChooseSecondProjectileType.performed -= ChooseSecondProjectileType;
            _playerControls.Player.Pause.performed -= Pause;
        }
        
        private void Move(InputAction.CallbackContext context)
            => MovementInput = context.ReadValue<Vector2>();

        private void Look(InputAction.CallbackContext context)
            => MousePosition = context.ReadValue<Vector2>();

        private void Attack(InputAction.CallbackContext context)
            => OnAttackPressed?.Invoke();

        private void ChooseFirstProjectileType(InputAction.CallbackContext context)
            => OnFirstProjectileTypeSelected?.Invoke();

        private void ChooseSecondProjectileType(InputAction.CallbackContext context)
            => OnSecondProjectileTypeSelected?.Invoke();

        private void Pause(InputAction.CallbackContext context) 
            => OnPausePressed?.Invoke();

        ~InputService()
        {
            Unsubscribe();
            _playerControls.Disable();
        }
    }
}
