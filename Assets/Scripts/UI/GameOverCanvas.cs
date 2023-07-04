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

    private void Start()
    {
        _victoryButton.onClick.AddListener(ReloadScene);
        _failureButton.onClick.AddListener(ReloadScene);
    }

    private void OnEnable()
    {
        EnemiesController.OnAllEnemiesKilled += ShowVictoryPanel;
        PlayerHealth.OnPlayerDied += ShowFailurePanel;
    }

    private void OnDisable()
    {
        EnemiesController.OnAllEnemiesKilled -= ShowVictoryPanel;
        PlayerHealth.OnPlayerDied -= ShowFailurePanel;
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
