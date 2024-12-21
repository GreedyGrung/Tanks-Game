using Assets.Scripts.Data;
using Assets.Scripts.Services.PersistentProgress;
using System;
using TankGame.Core.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

[SelectionBase]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, IDamageable, ISavedProgress
{
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private PlayerHealthData _healthData;

    private PlayerMovement _playerMovement;
    private IUIService _uiService;

    private string CurrentLevel => SceneManager.GetActiveScene().name;

    public IHealth Health { get; private set; }
    public PlayerWeapon Weapon => _playerWeapon;

    public void Init(IInputService inputService, IUIService uiService)
    {
        Health = new PlayerHealth(_healthData.MaxHealth, _healthData.MaxHealth);
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init(inputService);
        _playerWeapon.Init(inputService);
        _uiService = uiService;

        EnemiesController.OnAllEnemiesKilled += DeactivatePlayer;
        Health.OnDied += DeactivatePlayer;
        Health.OnDied += ShowFailurePanel;
    }

    public void UpdateProgress(PlayerProgress playerProgress)
    {
        playerProgress.PlayerData.Health = Health.Value;
        playerProgress.PlayerData.PositionOnLevel = new(CurrentLevel, transform.position.AsVectorData());
    }

    public void LoadProgress(PlayerProgress playerProgress)
    {
        if (CurrentLevel == playerProgress.PlayerData.PositionOnLevel.Level
            && playerProgress.PlayerData.PositionOnLevel.Position != null)
        {
            var savedPosition = playerProgress.PlayerData.PositionOnLevel.Position;
            transform.position = savedPosition.AsUnityVector3();
        }

        Health.SetValue(playerProgress.PlayerData.Health != 0 ? playerProgress.PlayerData.Health : Health.MaxValue);
    }

    public void TakeDamage(float damage)
    {
        Health.Subtract(damage);
    }

    private void ShowFailurePanel()
    {
        _uiService.Open(UIPanelId.FailurePanel);
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
        Health.OnDied -= ShowFailurePanel;
    }
}
