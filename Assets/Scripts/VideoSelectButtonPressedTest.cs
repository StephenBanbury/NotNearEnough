using UnityEngine;

namespace Assets.Scripts
{
    public class VideoSelectButtonPressedTest : MonoBehaviour
    {
        public int _videoId;

        //private VideoSelectSync _videoSelectSync;
        private VideoSelect _videoSelectDisplay;

        //public static VideoSelectButtonPressedTest instance;

        void Start()
        {
            // Get reference to the sync and action components
            //_videoSelectSync = gameObject.GetComponentInParent<VideoSelectSync>();
            _videoSelectDisplay = gameObject.GetComponentInParent<VideoSelect>();
        }

        public void OnPress()
        {
            _videoId = Random.Range(1, 16);

            _videoSelectDisplay.SetVideoId(_videoId);
            _videoSelectDisplay.KeepInSync(_videoId);
        }

    }
}