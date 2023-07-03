using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private PlayerHealthData _healthData;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = new(_healthData);
        _playerMovement.Init();
        _playerWeapon.Init();
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += DeactivatePlayer;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= DeactivatePlayer;
    }

    public void TakeDamage(float damage)
    {
        _playerHealth.SubtractHealth(damage);
    }

    private void DeactivatePlayer()
    {
        _playerMovement.enabled = false;
        _playerWeapon.enabled = false;
        gameObject.SetActive(false);
    }
}
