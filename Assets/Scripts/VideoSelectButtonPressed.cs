using UnityEngine;

namespace Assets.Scripts
{
    public class VideoSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _videoId;

        private VideoSelectDisplay _videoSelectDisplay;

        void Start()
        {
            _videoSelectDisplay = gameObject.GetComponentInParent<VideoSelectDisplay>();
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