using Assets.Scripts.Models;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Services;
using DG.Tweening;
using UnityEngine.Video;
using ScreenFormation = Assets.Scripts.Enums.ScreenFormation;

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
        //private ScreenFormation _lastSelectedScreenFormation;

        private List<GameObject> currentScreens = new List<GameObject>();

        private float _floorAdjust = 1.25f;

        [SerializeField] private VideoClip[] _videoClips = new VideoClip[5];
        [SerializeField] private Transform _selectButton;
        [SerializeField] private GameObject _screen;
        [SerializeField] private GameObject _screenVariant;


        public int SelectedVideo { set => _lastSelectedVideoId = value; }
        public int SelectedStream { set => _lastSelectedStreamId = value; }
        public int SelectedDisplay { set => _lastSelectedDisplayId = value; }
        public MediaType SelectedMediaType { set => _lastSelectedMediaType = value; }
        //public ScreenFormation SelectedScreenFormation { set => _lastSelectedScreenFormation = value; }

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

            SpawnScreens(ScreenFormation.LargeSquare);
        }

        public void CreateStreamSelectButtons()
        {
            var agoraUsers = AgoraController.instance.AgoraUsers;
            var selectPanel = GameObject.Find("StreamSelectorPanel");

            if (agoraUsers != null && selectPanel != null)
            {
                foreach (Transform child in selectPanel.transform)
                {
                    Destroy(child.gameObject);
                }

                var joinedUsers = agoraUsers.Where(u => !(u.IsLocal || u.LeftRoom)).ToList();

                Debug.Log($"Non-local agora users: {joinedUsers.Count}");

                var xPos = selectPanel.transform.position.x;
                var yStart = 0.55f;
                var zPos = selectPanel.transform.position.z;

                var i = 1;

                foreach (var joinedUser in joinedUsers)
                {
                    var buttonName = $"Button{i}";
                    var yPos = yStart - (i - 1) * 0.117f;
                    var button = Instantiate(_selectButton, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                    button.name = buttonName;
                    var buttonScript = button.gameObject.GetComponent<StreamSelectButtonPressed>();
                    buttonScript.StreamId = joinedUser.Id;
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
            //Debug.Log("In AssignVideoToDisplay");
            Debug.Log($"_lastSelectedVideoId: {_lastSelectedVideoId}");
            Debug.Log($"_lastSelectedDisplayId: {_lastSelectedDisplayId}");

            if (_lastSelectedVideoId > 0 && _lastSelectedDisplayId > 0 &&
                _displayVideo[_lastSelectedDisplayId].Id != _lastSelectedVideoId)
            {
                var video = _videos.FirstOrDefault(v => v.Id == _lastSelectedVideoId);
                video.Show = true;

                var screensContainerName = "Screens";
                var screenName = $"Screen {_lastSelectedDisplayId}";
                var screenVariantName = $"Screen Variant {_lastSelectedDisplayId}";

                // Using _displayVideo should be necessary only for URL based content
                _displayVideo[_lastSelectedDisplayId] = video;

                var screensContainer = GameObject.Find(screensContainerName);
                var screenObject = screensContainer.transform.Find(screenName);
                if (screenObject == null) screenObject = screensContainer.transform.Find(screenVariantName);


                Debug.Log($"Show video '{_displayVideo[_lastSelectedDisplayId].Title}' on display {screenObject.name}");

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
        
        public void SpawnScreens(ScreenFormation formation)
        {
            Debug.Log("SpawnScreens");

            var thisFormation = new List<ScreenPosition>();
            var screenFormation = new Services.ScreenFormation();

            switch (formation)
            {
                case ScreenFormation.LargeSquare: 
                    thisFormation = screenFormation.LargeSquare();
                    break;
                case ScreenFormation.SmallSquare:
                    thisFormation = screenFormation.SmallSquare();
                    break;
                case ScreenFormation.Cross:
                    thisFormation = screenFormation.Cross();
                    break;
                case ScreenFormation.Star:
                    thisFormation = screenFormation.Star();
                    break;
                case ScreenFormation.Circle:
                    thisFormation = screenFormation.Circle();
                    break;
                case ScreenFormation.Triangle:
                    thisFormation = screenFormation.Triangle();
                    break;
                case ScreenFormation.ShortRectangle:
                    thisFormation = screenFormation.ShortRectangle();
                    break;
                case ScreenFormation.LongRectangle:
                    thisFormation = screenFormation.LongRectangle();
                    break;
            }

            var screensContainer = GameObject.Find("Screens");

            if (screensContainer == null)
            {
                screensContainer = new GameObject { name = "Screens" };
            }

            // Destroy references to instantiated GameObjects
            foreach (var currentScreen in currentScreens)
            {
                GameObject.Destroy(currentScreen);
            }

            //var screenNumber = 1;

            foreach (var screenPosition in thisFormation)
            {
                //if (!screenPosition.Hide)
                //{
                    var vector3 = screenPosition.Vector3;
                    vector3.y += _floorAdjust;

                    GameObject screen;

                    if (screenPosition.Id % 2 != 0)
                    {
                        screen = Instantiate(_screen, vector3, Quaternion.identity);
                        screen.name = $"Screen {screenPosition.Id}";
                    }
                    else
                    {
                        screen = Instantiate(_screenVariant, vector3, Quaternion.identity);
                        screen.name = $"Screen Variant {screenPosition.Id}";
                    }

                    screen.transform.Rotate(0, screenPosition.Rotation, 0);
                    screen.transform.SetParent(screensContainer.transform);

                    //var rend = screen.GetComponent<MeshRenderer>();
                    //rend.enabled = screenPosition.Hide;

                    // Add to references for instantiated GameObjects to collection so they can be destroyed
                    currentScreens.Add(screen);
                //}

                //screenNumber++;
            }
        }

        public void TweenScreens(ScreenFormation formation, int tweenTimeSeconds)
        {
            var thisFormation = new List<ScreenPosition>();
            var screenFormation = new Services.ScreenFormation();

            switch (formation)
            {
                case ScreenFormation.LargeSquare:
                    thisFormation = screenFormation.LargeSquare();
                    break;
                case ScreenFormation.SmallSquare:
                    thisFormation = screenFormation.SmallSquare();
                    break;
                case ScreenFormation.Cross:
                    thisFormation = screenFormation.Cross();
                    break;
                case ScreenFormation.Star:
                    thisFormation = screenFormation.Star();
                    break;
                case ScreenFormation.Circle:
                    thisFormation = screenFormation.Circle();
                    break;
                case ScreenFormation.Triangle:
                    thisFormation = screenFormation.Triangle();
                    break;
                case ScreenFormation.ShortRectangle:
                    thisFormation = screenFormation.ShortRectangle();
                    break;
                case ScreenFormation.LongRectangle:
                    thisFormation = screenFormation.LongRectangle();
                    break;
            }

            var screensContainer = GameObject.Find("Screens");

            if (screensContainer == null)
            {
                screensContainer = new GameObject { name = "Screens" };
                SpawnScreens(formation);
                return;
            }

            foreach (var screenPosition in thisFormation)
            {
                var screenPositionPrev = currentScreens[screenPosition.Id - 1];

                var vector3To = screenPosition.Vector3;
                vector3To.y += _floorAdjust;

                screenPositionPrev.transform.DOMove(vector3To, tweenTimeSeconds);
                screenPositionPrev.transform.DORotate(new Vector3(0, screenPosition.Rotation, 0), 3);
            }
        }
    }
}
