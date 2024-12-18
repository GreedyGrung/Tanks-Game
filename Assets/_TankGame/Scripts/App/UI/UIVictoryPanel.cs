using UnityEngine;
using UnityEngine.SceneManagement;

public class UIVictoryPanel : UIPanelBase
{
    protected override void Close()
    {
        base.Close();

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
