using UnityEngine;

namespace TankGame.Runtime.UI.Buttons
{
    public class UIButtonExitGame : UIButtonBehaviourBase
    {
        protected override void HandleClick()
        {
            Application.Quit();
        }
    }
}