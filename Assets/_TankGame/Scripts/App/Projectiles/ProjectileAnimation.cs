using System;
using UnityEngine;

namespace _TankGame.App.Projectiles
{
    [RequireComponent(typeof(Animator))]
    public class ProjectileAnimation : MonoBehaviour
    {
        public event Action OnFinished;

        private Animator _animator;

        private void Awake() => _animator = GetComponent<Animator>();

        private void OnEnable() => PlayAnimation();

        public void PlayAnimation() => _animator.enabled = true;

        public virtual void DisableObject_AnimationEvent()
        {
            _animator.enabled = false;
            gameObject.SetActive(false);
            OnFinished?.Invoke();
        }
    }
}
