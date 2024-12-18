using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanelBase : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    protected virtual void Subscribe()
    {
        _closeButton.onClick.AddListener(Close);
    }

    protected virtual void Unsubscribe()
    {
        _closeButton.onClick.RemoveListener(Close);
    }

    protected virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
