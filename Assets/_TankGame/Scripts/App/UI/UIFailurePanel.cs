using UnityEngine.SceneManagement;

public class UIFailurePanel : UIPanelBase
{
    protected override void Close()
    {
        base.Close();

        SceneManager.LoadScene(0);
    }
}
