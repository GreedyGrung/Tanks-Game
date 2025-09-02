using DG.Tweening;
using UnityEngine;

namespace TankGame.Runtime.UI.Common
{
    [RequireComponent(typeof(RectTransform))]
    public class UIAnimationRotation : MonoBehaviour
    {
        [Header("Spin settings")]
        [SerializeField] private float _duration = 1f;
        [SerializeField] private int _loops = -1;
        [SerializeField] private Vector3 _spinEuler = new(0, 0, -360);
        [SerializeField] private RotateMode _rotateMode = RotateMode.FastBeyond360;

        private RectTransform _rect;
        private Tween _spinTween;

        private void Awake() => _rect = GetComponent<RectTransform>();

        private void OnEnable()  => StartSpin();
        
        private void OnDisable() => _spinTween?.Kill();

        private void StartSpin()
        {
            _spinTween = _rect
                .DOLocalRotate(_spinEuler, _duration, _rotateMode)
                .SetEase(Ease.Linear) 
                .SetLoops(_loops)
                .SetLink(gameObject);
        }

        [ContextMenu("Restart animation")]
        private void RestartAnimation()
        {
            _rect.rotation = Quaternion.identity;
            _spinTween?.Kill();
            StartSpin();
        }
    }
}