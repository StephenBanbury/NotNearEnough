using Assets.Scripts.Models;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Services;
using DG.Tweening;
using UnityEngine.Video;
namespace Assets.Scripts
{
    public class MediaDisplayManager: MonoBehaviour
    {
        public static MediaDisplayManager instance;
        
        private List<MediaDetail> _videos;
        private List<MediaDetail> _streams;

        private List<SceneDetail> _scenes;
        private int _sceneIndex;

        private Dictionary<int, MediaDetail> _displayVideo;
        private Dictionary<int, MediaDetail> _displayStream;

        private int _lastSelectedVideoId;
        private int _lastSelectedStreamId;

        private int _lastSelectedDisplayId;
        private MediaType _lastSelectedMediaType;

        private List<GameObject> _currentScreens = new List<GameObject>();

        private float _floorAdjust = 1.25f;

        [SerializeField] private VideoClip[] _videoClips = new VideoClip[5];
        [SerializeField] private Transform _selectButton;
        [SerializeField] private GameObject _screen;
        [SerializeField] private GameObject _screenVariant;
        [SerializeField] private AudioSource _screenAnimationAudio;


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

            _scenes = new List<SceneDetail>();
            _sceneIndex = 1;

            SpawnScreens(Scene.Scene1, ScreenFormation.LargeSquare);
            SpawnScreens(Scene.Scene2, ScreenFormation.ShortRectangle);
            SpawnScreens(Scene.Scene3, ScreenFormation.Circle);
            SpawnScreens(Scene.Scene4, ScreenFormation.Cross);
            SpawnScreens(Scene.Scene5, ScreenFormation.SmallSquare);
            SpawnScreens(Scene.Scene6, ScreenFormation.LongRectangle);
            SpawnScreens(Scene.Scene7, ScreenFormation.Star);
            SpawnScreens(Scene.Scene8, ScreenFormation.Triangle);

            Debug.Log("Scenes: -");
            foreach (var sceneDetail in _scenes)
            {
                Debug.Log($"Name: {sceneDetail.Name}, Formation: {sceneDetail.ScreenFormation}, Position: {sceneDetail.ScenePosition}");
            }

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
        
        public void SpawnScreens(Scene scene, ScreenFormation formation)
        {
            var thisFormation = new List<ScreenPosition>();
            var screenFormationService = new ScreenFormationService(scene);
            
            switch (formation)
            {
                case ScreenFormation.LargeSquare: 
                    thisFormation = screenFormationService.LargeSquare();
                    break;
                case ScreenFormation.SmallSquare:
                    thisFormation = screenFormationService.SmallSquare();
                    break;
                case ScreenFormation.Cross:
                    thisFormation = screenFormationService.Cross();
                    break;
                case ScreenFormation.Star:
                    thisFormation = screenFormationService.Star();
                    break;
                case ScreenFormation.Circle:
                    thisFormation = screenFormationService.Circle();
                    break;
                case ScreenFormation.Triangle:
                    thisFormation = screenFormationService.Triangle();
                    break;
                case ScreenFormation.ShortRectangle:
                    thisFormation = screenFormationService.ShortRectangle();
                    break;
                case ScreenFormation.LongRectangle:
                    thisFormation = screenFormationService.LongRectangle();
                    break;
            }

            var sceneName = $"Scene{_sceneIndex}";

            var sceneObject = GameObject.Find(sceneName);

            if (sceneObject != null)
            {
                GameObject.Destroy(sceneObject);
            }

            Debug.Log($"{sceneName} does not exist: creating {sceneName}");

            var scenePosition = screenFormationService.ScenePosition;

            sceneObject = Instantiate(new GameObject { name = sceneName }, scenePosition, Quaternion.identity);

            _scenes.Add(new SceneDetail
            {
                Id = _sceneIndex,
                Scene = scene,
                Name = sceneName,
                ScreenFormation = formation,
                ScenePosition = scenePosition,
                CurrentScreens = new List<GameObject>()
            });

            foreach (var screenPosition in thisFormation)
            {
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
                    screen.transform.SetParent(sceneObject.transform);

                    var currentScene = _scenes.First(s => s.Id == _sceneIndex);
                    currentScene.CurrentScreens.Add(screen);
            }

            _sceneIndex++;
        }

        public void TweenScreens(Scene scene, ScreenFormation newFormation, int tweenTimeSeconds)
        {
            var thisFormation = new List<ScreenPosition>();
            var screenFormationService = new ScreenFormationService(scene);

            switch (newFormation)
            {
                case ScreenFormation.LargeSquare:
                    thisFormation = screenFormationService.LargeSquare();
                    break;
                case ScreenFormation.SmallSquare:
                    thisFormation = screenFormationService.SmallSquare();
                    break;
                case ScreenFormation.Cross:
                    thisFormation = screenFormationService.Cross();
                    break;
                case ScreenFormation.Star:
                    thisFormation = screenFormationService.Star();
                    break;
                case ScreenFormation.Circle:
                    thisFormation = screenFormationService.Circle();
                    break;
                case ScreenFormation.Triangle:
                    thisFormation = screenFormationService.Triangle();
                    break;
                case ScreenFormation.ShortRectangle:
                    thisFormation = screenFormationService.ShortRectangle();
                    break;
                case ScreenFormation.LongRectangle:
                    thisFormation = screenFormationService.LongRectangle();
                    break;
            }

            var thisScene = _scenes.FirstOrDefault(s => s.Scene == scene);

            if (thisScene != null)
            {

                foreach (var screenPosition in thisFormation)
                {
                    var screenPositionPrev = thisScene.CurrentScreens[screenPosition.Id - 1];

                    var vector3To = screenPosition.Vector3;
                    vector3To.y += _floorAdjust;

                    _screenAnimationAudio.Play();

                    screenPositionPrev.transform.DOMove(vector3To, tweenTimeSeconds).SetEase(Ease.Linear);
                    screenPositionPrev.transform.DORotate(new Vector3(0, screenPosition.Rotation, 0), 3)
                        .SetEase(Ease.Linear);
                }
            }
        }
    }
}
