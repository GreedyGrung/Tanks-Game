using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private PlayerHealthData _healthData;

    public IHealth Health { get; private set; }
    private PlayerMovement _playerMovement;

    public PlayerWeapon Weapon => _playerWeapon;

    public void Init(IInputService inputService)
    {
        Health = new PlayerHealth(_healthData);
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init(inputService);
        _playerWeapon.Init(inputService);

        EnemiesController.OnAllEnemiesKilled += DeactivatePlayer;
        Health.OnDied += DeactivatePlayer;
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
