using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class VideoSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _videoIdText;
        [SerializeField] private int _videoId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                MediaDisplayManager.instance.MediaSelect(videoId: _videoId);
                _videoIdText.text = _videoId.ToString();
            }
        }
    }
}