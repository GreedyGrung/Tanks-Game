using System.Collections;
using TMPro;
using UnityEngine;

namespace TankGame.Runtime.UI.Common
{
    public class GenericHint : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _message;

        public void Show(string message, float duration)
        {
            gameObject.SetActive(true);
            _message.text = message;

            StartCoroutine(HideWithDelay(duration));
        }

        private IEnumerator HideWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            gameObject.SetActive(false);
        }
    }
}