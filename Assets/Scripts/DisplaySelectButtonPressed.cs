using UnityEngine;

namespace Assets.Scripts
{
    public class DisplaySelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _displayId;

        private DisplaySelect _displaySelectDisplay;

        void Start()
        {
            _displaySelectDisplay = gameObject.GetComponentInParent<DisplaySelect>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                _displaySelectDisplay.SetDisplayId(_displayId);
                _displaySelectDisplay.KeepInSync();
            }
        }

    }
}