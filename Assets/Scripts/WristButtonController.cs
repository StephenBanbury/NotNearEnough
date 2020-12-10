using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class WristButtonController : MonoBehaviour
    {
        [SerializeField] private Text _messageText;
        private string[] _messages;
        private bool _show;

        void Awake()
        {
            _show = false;
            _messages = new[] { "Show", "Hide" };
            ShowCurrentMessage();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                ShowCurrentMessage();
            }
        }

        private void ShowCurrentMessage()
        {
            Debug.Log($"Show controller panel: {_messages[_show ? 1 : 0]}");
            _messageText.text = _messages[_show ? 1 : 0];
            foreach (var panel in GameObject.FindGameObjectsWithTag("SelectionPanel"))
            {
                Debug.Log($"in panel: {panel.gameObject.name}");

                foreach (Transform child in panel.transform)
                {
                    Debug.Log($"in panel: {child.gameObject.name}");
                    child.gameObject.SetActive(_show);
                }
            }
            _show = !_show;
        }

    }
}
