using Assets.Scripts.Models;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Services;
using UnityEngine.Video;

namespace Assets.Scripts
{
    public class VideoDisplayManager: MonoBehaviour
    {
        public static VideoDisplayManager instance;


        private List<MediaDetail> _videos;
        private Dictionary<int, MediaDetail> _displayVideo;

        private int _lastSelectedVideoId;
        private int _lastSelectedDisplayId;

        private VideoPlayer _videoPlayer;

        [SerializeField] private VideoClip[] _videoClips = new VideoClip[5];


        public int SelectedVideo { set => _lastSelectedVideoId = value; }
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
            GetVideosFromService();
        }

        private void GetVideosFromService()
        {
            // Getting videos from the service will be necessary for URL based content
            // It is not necessary at the moment as we are using local content.
            // TODO: Streaming will probably be another thing altogether!

            var videoService = new VideoService();
            _videos = videoService.GetVideos();

            _displayVideo = new Dictionary<int, MediaDetail>();

            for (var i = 1; i <= 16; i++)
            {
                _displayVideo.Add(i, new MediaDetail());
            }
        }

        public void AssignVideoToDisplay()
        {
            if (_lastSelectedVideoId > 0 && _lastSelectedDisplayId > 0 &&
                _displayVideo[_lastSelectedDisplayId].Id != _lastSelectedVideoId)
            {
                var video = _videos.FirstOrDefault(v => v.Id == _lastSelectedVideoId);
                video.Show = true;

                // Using _displayVideo should be necessary only for URL based content
                _displayVideo[_lastSelectedDisplayId] = video;
                Debug.Log($"Show video '{_displayVideo[_lastSelectedDisplayId].Title}' on display {_lastSelectedDisplayId}");

                var screensContainer = GameObject.Find("Screens");
                var vc = _videoClips[_lastSelectedVideoId - 1];
                var screenObject = screensContainer.transform.Find($"VideoScreen{_lastSelectedDisplayId}");
                _videoPlayer = screenObject.GetComponentInChildren<VideoPlayer>();
                _videoPlayer.clip = vc;
                _videoPlayer.Play();
            }
        }
    }
}
