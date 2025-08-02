using UnityEngine;

namespace TankGame.Runtime.Entities.Enemies
{
    public class EnemyVisuals : MonoBehaviour
    {
        [SerializeField] private EnemyExplosion _explosionAnimation;

        public void PlayExplosionAnimation()
            => _explosionAnimation.gameObject.SetActive(true);
    }
}