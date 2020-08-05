using UnityEngine;

namespace Assets.Scripts
{
    public class VideoSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _videoId;

        private VideoSelect _videoSelectDisplay;

        void Start()
        {
            _videoSelectDisplay = gameObject.GetComponentInParent<VideoSelect>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                _videoSelectDisplay.SetVideoId(_videoId);
                _videoSelectDisplay.KeepInSync(_videoId);
            }
        }

    }
}