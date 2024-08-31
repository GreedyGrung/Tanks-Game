using UnityEngine;

public class EnemyVisuals : MonoBehaviour
{
    [SerializeField] private EnemyExplosion _explosionAnimation;

    public void PlayExplosionAnimation()
    {
        _explosionAnimation.gameObject.SetActive(true);
    }
}
