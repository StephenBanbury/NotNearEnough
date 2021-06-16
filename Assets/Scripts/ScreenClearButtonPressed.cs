using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScreenClearButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _screenId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                MediaDisplayManager.instance.ScreenClear(_screenId);
            }
        }
    }
}