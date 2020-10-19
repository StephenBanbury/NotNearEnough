using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StreamSelect : MonoBehaviour
    {
        [SerializeField] private Text _streamIdText;

        private int _streamId;
        private int _previousId;
        
        private StreamSelectSync _streamSelectSync;


        void Start()
        {
            _streamSelectSync = gameObject.GetComponentInParent<StreamSelectSync>();
        }

        public void SetStreamId(int id)
        {
            _streamId = id;

            if (_streamId > 0) // && _streamId != _previousId)
            {
                _streamIdText.text = _streamId.ToString();
                MediaDisplayManager.instance.SelectedStream = _streamId;
                MediaDisplayManager.instance.SelectedMediaType = MediaType.VideoStream;
            }
        }

        public void KeepInSync()
        {
            // If the id has changed, call SetId on the sync component
            if (_streamId != _previousId)
            {
                _streamSelectSync.SetId(_streamId);
                _previousId = _streamId;
            }
        }
    }
}