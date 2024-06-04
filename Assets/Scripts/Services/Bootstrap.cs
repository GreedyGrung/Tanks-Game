using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UnityActionsInputService _actionsInputService;

    private void Awake()
    {
        _player.Init(_actionsInputService);
    }
}
