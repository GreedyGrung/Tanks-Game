using UnityEngine.SceneManagement;

namespace TankGame.App.UI
{
    public class UIFailurePanel : UIPanelBase
    {
        protected override void Close()
        {
            base.Close();

            SceneManager.LoadScene(0);
        }
    }
}
