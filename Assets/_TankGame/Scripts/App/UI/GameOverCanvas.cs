using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    [Header("Victory Panel")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private Button _victoryButton;

    [Header("Failure Panel")]
    [SerializeField] private GameObject _failurePanel;
    [SerializeField] private Button _failureButton;

    private Player _player;

    private void Start()
    {
        _victoryButton.onClick.AddListener(ReloadSceneAfterVictory);
        _failureButton.onClick.AddListener(ReloadScene);

        LoadLevelState.OnPlayerSpawned += RecievePlayer;
        EnemiesController.OnAllEnemiesKilled += ShowVictoryPanel;
    }

    private void RecievePlayer(Player player)
    {
        _player = player;
        _player.Health.OnDied += ShowFailurePanel;
    }

    private void OnDestroy()
    {
        _victoryButton.onClick.RemoveListener(ReloadSceneAfterVictory);
        _failureButton.onClick.RemoveListener(ReloadScene);

        EnemiesController.OnAllEnemiesKilled -= ShowVictoryPanel;
        LoadLevelState.OnPlayerSpawned -= RecievePlayer;
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
        SceneManager.LoadScene(0);
    }

    private void ReloadSceneAfterVictory()
    {
        PlayerPrefs.DeleteAll();
        ReloadScene();
    }
}
