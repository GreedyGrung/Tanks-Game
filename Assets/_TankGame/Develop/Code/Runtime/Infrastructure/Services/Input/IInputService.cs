using System;
using UnityEngine;

namespace TankGame.Runtime.Infrastructure.Services.Input
{
    public interface IInputService
    {
        public event Action OnAttackPressed;
        public event Action OnFirstProjectileTypeSelected;
        public event Action OnSecondProjectileTypeSelected;

        public Vector2 MovementInput { get; }
        public Vector2 MousePosition { get; }
    }
}
