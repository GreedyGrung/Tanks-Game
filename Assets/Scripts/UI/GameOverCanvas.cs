using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameOverCanvas : MonoBehaviour
{
    [Header("Victory Panel")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private Button _victoryButton;

    [Header("Failure Panel")]
    [SerializeField] private GameObject _failurePanel;
    [SerializeField] private Button _failureButton;

    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        _victoryButton.onClick.AddListener(ReloadScene);
        _failureButton.onClick.AddListener(ReloadScene);

        EnemiesController.OnAllEnemiesKilled += ShowVictoryPanel;
        _player.Health.OnDied += ShowFailurePanel;
    }

    private void OnDestroy()
    {
        EnemiesController.OnAllEnemiesKilled -= ShowVictoryPanel;
        _player.Health.OnDied -= ShowFailurePanel;
    }

    private void ShowVictoryPanel()
    {
        _victoryPanel.SetActive(true);
    }

    private void ShowFailurePanel()
    {
        _failurePanel.SetActive(true);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
