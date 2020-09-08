using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
    public class OSCManager : MonoBehaviour
    {
        //private OscIn _oscIn;

        //void Start()
        //{
        //    _oscIn = gameObject.AddComponent<OscIn>();
        //    _oscIn.Open(7000);
        //    _oscIn.MapFloat("/test", OnTest);

        //}

        //void OnTest(float value)
        //{
        //    Debug.Log($"OSC received: {value}");
        //}
        
        private VideoSelect _videoSelectDisplay;
        private StreamSelect _streamSelectDisplay;
        private DisplaySelect _displaySelectDisplay;

        void Start()
        {
            _videoSelectDisplay = gameObject.GetComponentInParent<VideoSelect>();
            _streamSelectDisplay = gameObject.GetComponentInParent<StreamSelect>();
            _displaySelectDisplay = gameObject.GetComponentInParent<DisplaySelect>();
        }
        
        public void OnReceiveVideoId(int value)
        {
            Debug.Log($"VideoId: {value}");
            _videoSelectDisplay.SetVideoId(value);
            _videoSelectDisplay.KeepInSync();
        }

        public void OnReceiveStreamId(int value)
        {
            Debug.Log($"StreamId: {value}");
            _streamSelectDisplay.SetStreamId(value);
            _streamSelectDisplay.KeepInSync();
        }

        public void OnReceiveDisplayId(int value)
        {
            Debug.Log($"DisplayId: {value}");
            _displaySelectDisplay.SetDisplayId(value);
            _displaySelectDisplay.KeepInSync();
        }
    }
}
