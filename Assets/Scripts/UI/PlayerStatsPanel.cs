using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerStatsPanel : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image _healthValue;
    [SerializeField] private Image _reloadValue;
    [SerializeField] private Image _apProjectileBackground;
    [SerializeField] private Image _hexProjectileBackground;

    [Header("Colors")]
    [SerializeField] private Color _activeProjectile;
    [SerializeField] private Color _inactiveProjectile;

    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        SetupPanel();
        SubscribeToPlayerEvents();
    }

    private void SubscribeToPlayerEvents()
    {
        _player.Health.OnValueChanged += ChangePlayerHealthValue;
        _player.Weapon.OnPlayerShot += StartReloading;
        _player.Weapon.OnHexProjectileTypeChosen += ChooseHexProjectileType;
        _player.Weapon.OnApProjectileTypeChosen += ChooseApProjectileType;
    }

    private void OnDestroy()
    {
        _player.Health.OnValueChanged -= ChangePlayerHealthValue;
        _player.Weapon.OnPlayerShot -= StartReloading;
        _player.Weapon.OnHexProjectileTypeChosen -= ChooseHexProjectileType;
        _player.Weapon.OnApProjectileTypeChosen -= ChooseApProjectileType;
    }

    private void SetupPanel()
    {
        ChooseApProjectileType();
        _healthValue.fillAmount = 1;
        _reloadValue.fillAmount = 1;
    }

    private void ChangePlayerHealthValue(float currentHealth, float maxHealth)
    {
        _healthValue.fillAmount = currentHealth / maxHealth;
    }

    private void StartReloading()
    {
        _reloadValue.fillAmount = 0;
        StartCoroutine(FillReloadingBar());
    }

    private IEnumerator FillReloadingBar()
    {
        float timer = 0f;

        while (timer < _player.Weapon.WeaponData.ReloadTime)
        {
            timer += Time.deltaTime;
            float progress = timer / _player.Weapon.WeaponData.ReloadTime;
            _reloadValue.fillAmount = progress;
            yield return null;
        }

        _reloadValue.fillAmount = 1;
    } 

    private void ChooseHexProjectileType()
    {
        _apProjectileBackground.color = _inactiveProjectile;
        _hexProjectileBackground.color = _activeProjectile;
    }

    private void ChooseApProjectileType()
    {
        _apProjectileBackground.color = _activeProjectile;
        _hexProjectileBackground.color = _inactiveProjectile;
    }
}
