using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHolder : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public Vector2 MousePosition { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnMouseInput(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }
}
