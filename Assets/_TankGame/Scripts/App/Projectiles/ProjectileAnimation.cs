using UnityEngine;

namespace TankGame.App.Projectiles
{
    [RequireComponent(typeof(Animator))]
    public class ProjectileAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Projectile _projectile;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _projectile = GetComponentInParent<Projectile>();
        }

        private void OnEnable()
        {
            PlayAnimation();
        }

        public void PlayAnimation()
        {
            _animator.enabled = true;
        }

        public virtual void DisableObject_AnimationEvent()
        {
            _animator.enabled = false;
            gameObject.SetActive(false);
            _projectile.gameObject.SetActive(false);
        }
    }
}
