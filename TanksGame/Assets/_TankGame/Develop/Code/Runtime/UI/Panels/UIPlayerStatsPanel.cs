using TankGame.Runtime.Entities.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Runtime.UI.Panels
{
    public class UIPlayerStatsPanel : MonoBehaviour
    {
        [Header("Images")]
        [SerializeField] private Image _healthValue;
        [SerializeField] private Image _reloadValue;
        [SerializeField] private Image _apProjectileBackground;
        [SerializeField] private Image _hexProjectileBackground;

        [Header("Colors")]
        [SerializeField] private Color _activeProjectile;
        [SerializeField] private Color _inactiveProjectile;

        private IPlayer _player;

        public void Initialize(IPlayer player)
        {
            _player = player;
            SetupPanel();
            SubscribeToPlayerEvents();
        }

        private void SubscribeToPlayerEvents()
        {
            _player.Health.OnValueChanged += ChangePlayerHealthValue;
            _player.Weapon.OnHexProjectileTypeChosen += ChooseHexProjectileType;
            _player.Weapon.OnApProjectileTypeChosen += ChooseApProjectileType;
        }

        private void OnDestroy()
        {
            _player.Health.OnValueChanged -= ChangePlayerHealthValue;
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

        private void Update()
        {
            if (_player == null || _player.Deactivated) return;
            
            _reloadValue.fillAmount = _player.Weapon.ReloadProgress;
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
}
