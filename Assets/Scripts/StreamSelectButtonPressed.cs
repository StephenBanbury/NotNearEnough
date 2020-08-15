using UnityEngine;

namespace Assets.Scripts
{
    public class StreamSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _streamId;

        private StreamSelect _streamSelectDisplay;

        void Start()
        {
            _streamSelectDisplay = gameObject.GetComponentInParent<StreamSelect>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                _streamSelectDisplay.SetStreamId(_streamId);
                _streamSelectDisplay.KeepInSync();
            }
        }

    }
}