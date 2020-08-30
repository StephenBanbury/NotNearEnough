using Assets.Scripts.Models;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using agora_gaming_rtc;
using Assets.Scripts.Enums;
using Assets.Scripts.Services;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts
{
    public class MediaDisplayManager: MonoBehaviour
    {
        public static MediaDisplayManager instance;
        
        private List<MediaDetail> _videos;
        private List<MediaDetail> _streams;

        private Dictionary<int, MediaDetail> _displayVideo;
        private Dictionary<int, MediaDetail> _displayStream;

        private int _lastSelectedVideoId;
        private int _lastSelectedStreamId;

        private int _lastSelectedDisplayId;
        private MediaType _lastSelectedMediaType;
        
        [SerializeField] private VideoClip[] _videoClips = new VideoClip[5];
        [SerializeField] private Transform _selectButton;


        public int SelectedVideo { set => _lastSelectedVideoId = value; }
        public int SelectedStream { set => _lastSelectedStreamId = value; }
        public int SelectedDisplay { set => _lastSelectedDisplayId = value; }
        public MediaType SelectedMediaType { set => _lastSelectedMediaType = value; }

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
            CreateStreamSelectButtons();
        }

        public void CreateStreamSelectButtons()
        {
            var agoraUsers = AgoraController.instance.AgoraUsers;

            if (agoraUsers != null)
            {
                var joinedUsers = agoraUsers.Where(u => !(u.IsLocal || u.LeftRoom)).ToList();

                var selectPanel = GameObject.Find("StreamSelectorPanel");
                foreach (Transform child in selectPanel.transform)
                {
                    Destroy(child.gameObject);
                }

                Debug.Log($"joinedUsers: {joinedUsers.Count}");

                var xPos = selectPanel.transform.position.x;
                var yStart = 1.666f;
                var zPos = selectPanel.transform.position.z - 0.04f;

                var i = 1;

                foreach (var joinedUser in joinedUsers)
                {
                    var id = joinedUser.Id;
                    var buttonName = $"Button{i}";
                    var yPos = yStart - (i - 1) * 0.117f;
                    var button = Instantiate(_selectButton, new Vector3(xPos, yPos, zPos),
                        _selectButton.transform.rotation);
                    button.name = buttonName;
                    var buttonScript = button.gameObject.GetComponent<StreamSelectButtonPressed>();
                    buttonScript.StreamId = id;
                    button.transform.SetParent(selectPanel.transform);
                    i++;
                }
            }
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

        public void AssignMediaToDisplay()
        {
            switch (_lastSelectedMediaType)
            {
                case MediaType.VideoClip:
                    Debug.Log($"Assign video clip {_lastSelectedVideoId} to display {_lastSelectedDisplayId}");
                    AssignVideoToDisplay();
                    break;
                case MediaType.VideoStream:
                    Debug.Log($"Assign video stream {_lastSelectedStreamId} to display {_lastSelectedDisplayId}");
                    AssignStreamToDisplay();
                    break;
            }
        }

        private void AssignVideoToDisplay()
        {
            if (_lastSelectedVideoId > 0 && _lastSelectedDisplayId > 0 &&
                _displayVideo[_lastSelectedDisplayId].Id != _lastSelectedVideoId)
            {
                var video = _videos.FirstOrDefault(v => v.Id == _lastSelectedVideoId);
                video.Show = true;

                // Using _displayVideo should be necessary only for URL based content
                _displayVideo[_lastSelectedDisplayId] = video;
                Debug.Log($"Show video '{_displayVideo[_lastSelectedDisplayId].Title}' on display {_lastSelectedDisplayId}");

                var screensContainer = GameObject.Find("Screens A Cross");
                var screenObject = screensContainer.transform.Find($"Screen A {_lastSelectedDisplayId}");
                if (screenObject == null) screenObject = screensContainer.transform.Find($"Screen A Variant {_lastSelectedDisplayId}");

                var videoDisplay = screenObject.transform.Find("VideoDisplay");
                var canvasDisplay = screenObject.transform.Find("CanvasDisplay");

                videoDisplay.gameObject.SetActive(true);
                canvasDisplay.gameObject.SetActive(false);

                var videoPlayer = videoDisplay.GetComponentInChildren<VideoPlayer>();
                var vc = _videoClips[_lastSelectedVideoId - 1];
                videoPlayer.clip = vc;
                videoPlayer.Play();
            }
        }

        private void AssignStreamToDisplay()
        {
            if (_lastSelectedStreamId > 0 && _lastSelectedDisplayId > 0)
            {
                var agoraUsers = AgoraController.instance.AgoraUsers;

                if (agoraUsers.Any())
                {
                    Debug.Log("agoraUsers: -");
                    foreach (var user in agoraUsers)
                    {
                        Debug.Log($" - {user.Uid} (isLocal: {user.IsLocal}, leftRoom: {user.LeftRoom}, id: {user.Id})");
                    }

                    var agoraUser = agoraUsers.FirstOrDefault(u => u.Id == _lastSelectedStreamId);

                    Debug.Log($"agoraUser exists: {agoraUser != null}");

                    if (agoraUser != null)
                    {
                        //var screensContainer = GameObject.Find("Screens");
                        //var screenObject = screensContainer.transform.Find($"StreamingScreen{_lastSelectedDisplayId}");

                        //Debug.Log($"screenObject: {screenObject.name}");

                        //var videoDisplay = screenObject.transform.Find("VideoDisplay");
                        //var canvasDisplay = screenObject.transform.Find("CanvasDisplay");

                        //videoDisplay.gameObject.SetActive(false);
                        //canvasDisplay.gameObject.SetActive(true);

                        //Debug.Log($"canvasDisplay: {canvasDisplay.name}");

                        if (agoraUser.IsLocal || agoraUser.LeftRoom)
                        {
                            Debug.Log(
                                $"Something has gone wrong - is local: {agoraUser.IsLocal}, left room: {agoraUser.LeftRoom}.");
                        }
                        else
                        {
                            agoraUser.DisplayId = _lastSelectedDisplayId;
                            AgoraController.instance.AssignStreamToDisplay(agoraUser);
                        }
                    }
                }
            }
        }
    }
}
