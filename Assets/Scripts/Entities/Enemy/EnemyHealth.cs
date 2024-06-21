using System;
using UnityEngine;

public class EnemyHealth : IHealth
{
    public event Action OnDied;
    public event Action<float, float> OnValueChanged;

    public EnemyHealth(float value)
    {
        Value = value;
        MaxValue = value;
    }

    public float Value { get; private set; }
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
        OnValueChanged?.Invoke(Value, MaxValue);
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
            OnDied?.Invoke();
        }
    }

    public void KillImmediately()
    {
        Value = 0;
        OnDied?.Invoke();
        OnValueChanged?.Invoke(Value, MaxValue);
    }

    public void RestoreAll()
    {
        Value = MaxValue;
        OnValueChanged?.Invoke(Value, MaxValue);
    }

    public void SetValue(float value)
    {
        throw new NotImplementedException();
    }
}
