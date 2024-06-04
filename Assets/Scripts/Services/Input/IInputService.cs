using System;
using UnityEngine;

public interface IInputService
{
    public event Action OnLeftMouseButtonClicked;
    public event Action OnFirstProjectileTypeSelected;
    public event Action OnSecondProjectileTypeSelected;

    public Vector2 MovementInput { get; }
    public Vector2 MousePosition { get; }
}
