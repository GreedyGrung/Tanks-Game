using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UnityActionsInputService _actionsInputService;

    private Game _game;

    private void Awake()
    {
        _game = new Game();
        DontDestroyOnLoad(this);
        _player.Init(_actionsInputService);
    }
}
