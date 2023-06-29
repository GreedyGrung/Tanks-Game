using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHolder : MonoBehaviour
{
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
            Debug.Log("true");
        }
        else
        {
            MouseLeftButtonClick = false;
        }
    }
}
