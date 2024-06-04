using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnEnable()
    {
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        _animator.enabled = true;
    }

    public virtual void DisableObject()
    {
        _animator.enabled = false;
        gameObject.SetActive(false);
        _enemy.gameObject.SetActive(false);
    }
}
