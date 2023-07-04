using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Image _healthValue;
    [SerializeField] private Vector3 _offset;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _enemy.OnEnemyTookDamage += SetHealthValue;
    }

    private void OnDisable()
    {
        _enemy.OnEnemyTookDamage -= SetHealthValue;
    }

    private void Update()
    {
        transform.rotation = _mainCamera.transform.rotation;
        transform.position = _enemy.transform.position + _offset;
    }

    private void SetHealthValue(float currentHealth, float maxHealth)
    {
        _healthValue.fillAmount = currentHealth / maxHealth;
    }
}
