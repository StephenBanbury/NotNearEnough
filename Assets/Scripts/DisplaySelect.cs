using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DisplaySelect : MonoBehaviour
    {
        [SerializeField] private Text _displayIdText;

        private int _displayId;
        private int _previousId;

        private DisplaySelectSync _displaySelectSync;


        void Start()
        {
            //_displaySelectSync = gameObject.GetComponentInParent<DisplaySelectSync>();
            _displaySelectSync = gameObject.GetComponent<DisplaySelectSync>();
        }

        public void SetDisplayId(int id)
        {
            _displayId = id;
            
            if (_displayId > 0) // && _displayId != _previousId)
            {
                //_displayIdText.text = _displayId.ToString();
                MediaDisplayManager.instance.SelectedDisplay = _displayId;
            }
        }

        public void KeepInSync()
        {
            // If the id has changed, call SetId on the sync component
            if (_displayId != _previousId)
            {
                Debug.Log($"displayId: {_displayId}");
                _displaySelectSync.SetId(_displayId);
                _previousId = _displayId;
            }
        }
    }
}