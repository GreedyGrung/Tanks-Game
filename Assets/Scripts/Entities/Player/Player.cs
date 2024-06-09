using System;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, IDamageable
{
    public event Action OnInitialized;

    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private PlayerHealthData _healthData;

    private PlayerMovement _playerMovement;

    public IHealth Health { get; private set; }
    public PlayerWeapon Weapon => _playerWeapon;

    public void Init(IInputService inputService)
    {
        Health = new PlayerHealth(_healthData.MaxHealth);
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init(inputService);
        _playerWeapon.Init(inputService);

        EnemiesController.OnAllEnemiesKilled += DeactivatePlayer;
        Health.OnDied += DeactivatePlayer;

        OnInitialized?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        Health.Subtract(damage);
    }

    private void DeactivatePlayer()
    {
        _playerMovement.enabled = false;
        _playerWeapon.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EnemiesController.OnAllEnemiesKilled -= DeactivatePlayer;
        Health.OnDied -= DeactivatePlayer;
    }
}
