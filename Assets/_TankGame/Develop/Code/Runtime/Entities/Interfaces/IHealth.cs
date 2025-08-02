using System;

namespace TankGame.Runtime.Entities.Interfaces
{
    public interface IHealth
    {
        float MaxValue { get; }
        float Value { get; }
        bool IsFull { get; }
        bool IsDead { get; }

        event Action OnDied;
        event Action<float, float> OnValueChanged;

        void Subtract(float damage);
        void Add(float value);
        void RestoreAll();
        void KillImmediately();
        void SetValue(float value);
    }
}