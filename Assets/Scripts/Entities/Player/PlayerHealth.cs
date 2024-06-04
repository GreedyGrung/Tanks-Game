using UnityEngine;
using System;

public class PlayerHealth : IHealth
{
    public event Action<float, float> OnValueChanged;
    public event Action OnDied;

    public float Value { get; private set; }
    public float MaxValue { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsFull => Value == MaxValue;

    public PlayerHealth(PlayerHealthData data)
    {
        MaxValue = data.MaxHealth;
        Value = MaxValue;
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
        OnValueChanged?.Invoke(Value, MaxValue);

        if (Value <= 0)
        {
            Value = 0;
            IsDead = true;
            OnDied?.Invoke();
        }
    }

    public void Add(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException(nameof(value));
        }

        Value += value;
        Mathf.Clamp(Value, 0, MaxValue);
        OnValueChanged?.Invoke(Value, MaxValue);
    }

    public void KillImmediately()
    {
        Value = 0;
        OnDied?.Invoke();
        IsDead = true;
        OnValueChanged?.Invoke(Value, MaxValue);
    }

    public void RestoreAll()
    {
        Value = MaxValue;
        OnValueChanged?.Invoke(Value, MaxValue);
    }
}
