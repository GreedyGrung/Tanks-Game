using Assets.Scripts.Data;
using Assets.Scripts.Services.PersistentProgress;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, IDamageable, ISavedProgress
{
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private PlayerHealthData _healthData;

    private PlayerMovement _playerMovement;

    public IHealth Health { get; private set; }
    public PlayerWeapon Weapon => _playerWeapon;

    public void Init(IInputService inputService)
    {
        Health = new PlayerHealth(_healthData.MaxHealth, _healthData.MaxHealth);
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init(inputService);
        _playerWeapon.Init(inputService);

        EnemiesController.OnAllEnemiesKilled += DeactivatePlayer;
        Health.OnDied += DeactivatePlayer;
    }

    public void UpdateProgress(PlayerProgress playerProgress)
    {
        playerProgress.Health = Health.Value;
    }

    public void LoadProgress(PlayerProgress playerProgress)
    {
        Health.SetValue(playerProgress.Health != 0 ? playerProgress.Health : Health.MaxValue);
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
