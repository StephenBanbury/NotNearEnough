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
            _messageText.text = _messages[_show ? 1 : 0];
            ShowPanel();
            _show = !_show;
        }

        private void ShowPanel()
        {
            foreach (var panel in GameObject.FindGameObjectsWithTag("SelectionPanel"))
            {
                foreach (Transform child in panel.transform)
                {
                    child.gameObject.SetActive(_show);
                }
            }
        }
    }
}
