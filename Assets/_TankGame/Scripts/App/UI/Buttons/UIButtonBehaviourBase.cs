using UnityEngine;
using UnityEngine.UI;

namespace TankGame.App.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButtonBehaviourBase : MonoBehaviour
    {
        protected Button Button { get; private set; }

        private void Awake()
        {
            Button = GetComponent<Button>();

            Initialize();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        protected virtual void Initialize() { }

        protected abstract void HandleClick();

        private void Subscribe()
        {
            Button.onClick.AddListener(HandleClick);
        }

        private void Unsubscribe()
        {
            Button.onClick.RemoveListener(HandleClick);
        }
    }
}
