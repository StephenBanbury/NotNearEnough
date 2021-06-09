using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScreenSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _screenIdText;
        [SerializeField] private int _screenId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                MediaDisplayManager.instance.ScreenSelect(_screenId);
                _screenIdText.text = _screenId.ToString();
                //Debug.Log($"ScreenSelectButtonPressed: {_screenId}");
            }
        }
    }
}