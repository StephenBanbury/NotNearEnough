using UnityEngine;

namespace Assets.Scripts
{
    public class StreamSelectButtonPressed : MonoBehaviour
    {
        public int StreamId;

        private StreamSelect _streamSelectDisplay;

        void Start()
        {
            _streamSelectDisplay = gameObject.GetComponentInParent<StreamSelect>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Selected StreamId: {StreamId}");
            if (other.CompareTag("Hand"))
            {
                _streamSelectDisplay.SetStreamId(StreamId);
                _streamSelectDisplay.KeepInSync();
            }
        }

    }
}