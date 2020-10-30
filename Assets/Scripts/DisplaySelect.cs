using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DisplaySelect : MonoBehaviour
    {
        private int _displayId;
        private int _previousId;

        private DisplaySelectSync _displaySelectSync;


        void Start()
        {
            _displaySelectSync = gameObject.GetComponent<DisplaySelectSync>();
        }

        public void SetDisplayId(int id)
        {
            _displayId = id;
            
            if (_displayId > 0 && _displayId != _previousId)
            {
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