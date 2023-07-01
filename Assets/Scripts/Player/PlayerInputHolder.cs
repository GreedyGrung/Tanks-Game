using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHolder : MonoBehaviour
{
    public event Action OnLeftMouseButtonClicked;
    public event Action OnFirstProjectileTypeSelected;
    public event Action OnSecondProjectileTypeSelected;

    public Vector2 MovementInput { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public bool MouseLeftButtonClick { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnMouseInput(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void OnMouseLeftButtonClick(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            MouseLeftButtonClick = true;
            OnLeftMouseButtonClicked?.Invoke();
        }
        else
        {
            MouseLeftButtonClick = false;
        }
    }

    public void ChooseFirstProjectileType(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            OnFirstProjectileTypeSelected?.Invoke();
        }
    }

    public void ChooseSecondProjectileType(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            OnSecondProjectileTypeSelected?.Invoke();
        }
    }
}
