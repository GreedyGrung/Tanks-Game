using System;
using UnityEngine;

namespace TankGame.Core.Services.Input
{
    public interface IInputService : IService
    {
        public event Action OnAttackPressed;
        public event Action OnFirstProjectileTypeSelected;
        public event Action OnSecondProjectileTypeSelected;

        public Vector2 MovementInput { get; }
        public Vector2 MousePosition { get; }
    }
}
