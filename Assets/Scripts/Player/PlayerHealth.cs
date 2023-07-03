using UnityEngine;
using System;

public class PlayerHealth
{
    public static event Action<float, float> OnPlayerHealthChanged;
    public static event Action OnPlayerDied;

    private float _maxHealth;
    private float _health;

    public PlayerHealth(PlayerHealthData data)
    {
        _maxHealth = data.MaxHealth;
        _health = _maxHealth;
    }

    public void SubtractHealth(float value)
    {
        _health -= value;
        OnPlayerHealthChanged?.Invoke(_health, _maxHealth);

        if (_health <= 0)
        {
            _health = 0;
            OnPlayerDied?.Invoke();
        }
    }

    public void AddHealth(float value)
    {
        _health += value;
        Mathf.Clamp(_health, 0, _maxHealth);
        OnPlayerHealthChanged?.Invoke(_health, _maxHealth);
    }
}
