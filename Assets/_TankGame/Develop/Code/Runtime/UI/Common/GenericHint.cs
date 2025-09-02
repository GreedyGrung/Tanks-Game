using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TankGame.Runtime.UI.Common
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GenericHint : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _message;
        [Space]
        [SerializeField, Min(0f)] private float _fadeIn  = 0.2f;
        [SerializeField, Min(0f)] private float _fadeOut = 0.15f;
        [SerializeField] private Ease _easeIn = Ease.InQuad;
        [SerializeField] private Ease _easeOut = Ease.OutQuad;
        
        private CanvasGroup _group;
        private Sequence _sequence;
        
        private void Awake()
        {
            _group = GetComponent<CanvasGroup>();
            
            _group.alpha = 0f;
            gameObject.SetActive(false);
        }
        
        private void OnDisable() => KillSequence();

        public void Show(string message, float duration)
        {
            gameObject.SetActive(true);
            _message.text = message;
            duration = Mathf.Max(0f, duration);
            
            KillSequence();
            
            _sequence = DOTween.Sequence()
                .SetAutoKill(true)
                .SetLink(gameObject)
                .Append(_group.DOFade(1f, _fadeIn).SetEase(_easeIn))
                .AppendInterval(duration)
                .Append(_group.DOFade(0f, _fadeOut).SetEase(_easeOut))
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    _sequence = null;
                });
        }

        private void KillSequence()
        {
            if (_sequence != null && _sequence.IsActive())
            {
                _sequence.Kill();
            }

            _sequence = null;
        }
    }
}