using TankGame.App.Entities.Enemies.Base;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.App.Entities.Enemies
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthValue;
        [SerializeField] private Vector3 _offset;

        private Enemy _enemy;
        private Camera _mainCamera;

        private void Start() => _mainCamera = Camera.main;

        private void Update()
        {
            if (_enemy == null) return;

            transform.rotation = _mainCamera.transform.rotation;
            transform.position = _enemy.transform.position + _offset;
        }

        private void OnDestroy()
            => _enemy.Health.OnValueChanged -= SetHealthValue;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
            _enemy.Health.OnValueChanged += SetHealthValue;
        }

        private void SetHealthValue(float currentHealth, float maxHealth)
            => _healthValue.fillAmount = currentHealth / maxHealth;
    }
}