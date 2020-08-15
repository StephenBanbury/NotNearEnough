using Assets.Scripts.Models;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Services;
using UnityEngine.Video;

namespace Assets.Scripts
{
    public class StreamDisplayManager: MonoBehaviour
    {
        public static StreamDisplayManager instance;


        private List<MediaDetail> _streams;
        private Dictionary<int, MediaDetail> _displayStream;

        private int _lastSelectedStreamId;
        private int _lastSelectedDisplayId;

        //private StreamPlayer _streamPlayer;

        //[SerializeField] private StreamClip[] _streamClips = new StreamClip[5];


        public int SelectedStream { set => _lastSelectedStreamId = value; }
        public int SelectedDisplay { set => _lastSelectedDisplayId = value; }



        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            //GetStreamsFromService();
        }

        //private void GetStreamsFromService()
        //{
        //    // Getting streams from the service will be necessary for URL based content
        //    // It is not necessary at the moment as we are using local content.
        //    // TODO: Streaming will probably be another thing altogether!

        //    var streamService = new StreamService();
        //    _streams = streamService.GetStreams();

        //    _displayStream = new Dictionary<int, MediaDetail>();

        //    for (var i = 1; i <= 16; i++)
        //    {
        //        _displayStream.Add(i, new MediaDetail());
        //    }
        //}

        public void AssignStreamToDisplay()
        {
            //if (_lastSelectedStreamId > 0 && _lastSelectedDisplayId > 0 &&
            //    _displayStream[_lastSelectedDisplayId].Id != _lastSelectedStreamId)
            //{
                //var stream = _streams.FirstOrDefault(v => v.Id == _lastSelectedStreamId);
                //stream.Show = true;

                //// Using _displayStream should be necessary only for URL based content
                //_displayStream[_lastSelectedDisplayId] = stream;
                //Debug.Log($"Show stream '{_displayStream[_lastSelectedDisplayId].Title}' on display {_lastSelectedDisplayId}");

                //var screensContainer = GameObject.Find("Screens");
                //var vc = _streamClips[_lastSelectedStreamId - 1];
                //var screenObject = screensContainer.transform.Find($"StreamScreen{_lastSelectedDisplayId}");
                //_streamPlayer = screenObject.GetComponentInChildren<StreamPlayer>();
                //_streamPlayer.clip = vc;
                //_streamPlayer.Play();
            //}
        }
    }
}
