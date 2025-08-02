using System;
using TankGame.Runtime.Entities.Interfaces;
using UnityEngine;

namespace TankGame.Runtime.Entities.Player
{
    public class PlayerHealth : IHealth
    {
        public event Action<float, float> OnValueChanged;
        public event Action OnDied;

        private float _value;

        public PlayerHealth(float value, float maxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }

        public float Value
        {
            get => _value;
            private set
            {
                _value = value;
                OnValueChanged?.Invoke(Value, MaxValue);
            }
        }
        public float MaxValue { get; private set; }
        public bool IsDead => Value == 0;
        public bool IsFull => Value == MaxValue;

        public void Add(float value)
        {
            if (value < 0)
            {
                throw new ArgumentException(nameof(value));
            }

            Value += value;
            Mathf.Clamp(Value, 0, MaxValue);
        }

        public void Subtract(float damage)
        {
            if (damage < 0)
            {
                throw new ArgumentException(nameof(damage));
            }

            if (Value <= 0)
            {
                return;
            }

            Value -= damage;

            if (Value <= 0)
            {
                Value = 0;
                OnDied?.Invoke();
            }
        }

        public void KillImmediately()
        {
            Value = 0;
            OnDied?.Invoke();
        }

        public void RestoreAll()
        {
            Value = MaxValue;
        }

        public void SetValue(float value)
        {
            if (value < 0)
            {
                throw new ArgumentException(nameof(value));
            }

            Value = value;
        }
    }
}