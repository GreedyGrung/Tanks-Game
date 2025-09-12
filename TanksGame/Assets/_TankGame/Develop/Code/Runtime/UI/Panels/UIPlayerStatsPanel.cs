using R3;
using TankGame.Runtime.Entities.Interfaces;
using TankGame.Runtime.Utils.Enums;
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
        
        private readonly CompositeDisposable _disposables = new();

        public void Initialize(IPlayer player)
        {
            _player = player;
            SetupPanel();
            SubscribeToPlayerEvents();
        }

        private void SubscribeToPlayerEvents()
        {
            _player.Health.OnValueChanged += ChangePlayerHealthValue;

            _player.Weapon.ReloadProgress
                .Subscribe(value => _reloadValue.fillAmount = value)
                .AddTo(_disposables);
            
            _player.Weapon.SelectedProjectile
                .Subscribe(SelectProjectile)
                .AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _player.Health.OnValueChanged -= ChangePlayerHealthValue;
            
            _disposables.Dispose();
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

        private void SelectProjectile(ProjectileTypeId value)
        {
            switch (value)
            {
                case ProjectileTypeId.AP:
                    ChooseApProjectileType();
                    break;
                case ProjectileTypeId.HEX:
                    ChooseHexProjectileType();
                    break;
            }
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
