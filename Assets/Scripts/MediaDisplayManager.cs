using Assets.Scripts.Models;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        private int _sceneIndex;

        private Dictionary<int, MediaDetail> _displayVideo;
        private Dictionary<int, MediaDetail> _displayStream;

        private int _lastSelectedVideoId;
        private int _lastSelectedStreamId;

        private int _lastSelectedDisplayId;
        private MediaType _lastSelectedMediaType;

        private float _floorAdjust = 1.25f;

        [SerializeField] private VideoClip[] _videoClips = new VideoClip[5];
        [SerializeField] private Transform _selectButton;
        [SerializeField] private Transform _sceneObject;
        [SerializeField] private GameObject _screen;
        [SerializeField] private GameObject _screenVariant;
        [SerializeField] private AudioSource _sceneAudio;
        [SerializeField] private GameObject _sceneLights;
        [SerializeField] private GameObject _selectionPanels;


        public int SelectedVideo { set => _lastSelectedVideoId = value; }
        public int SelectedStream { set => _lastSelectedStreamId = value; }
        public int SelectedDisplay { set => _lastSelectedDisplayId = value; }
        public MediaType SelectedMediaType { set => _lastSelectedMediaType = value; }
        public List<SceneDetail> Scenes { get; private set; }
        public Scene MyCurrentScene { get; set; }

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

            Scenes = new List<SceneDetail>();
            _sceneIndex = 1;

            SpawnScene(Scene.Scene1, ScreenFormation.LargeSquare);
            SpawnScene(Scene.Scene2, ScreenFormation.ShortRectangle);
            SpawnScene(Scene.Scene3, ScreenFormation.Circle);
            SpawnScene(Scene.Scene4, ScreenFormation.Cross);
            SpawnScene(Scene.Scene5, ScreenFormation.SmallSquare);
            SpawnScene(Scene.Scene6, ScreenFormation.LongRectangle);
            SpawnScene(Scene.Scene7, ScreenFormation.Star);
            SpawnScene(Scene.Scene8, ScreenFormation.Triangle);

            //Debug.Log("Scenes: -");
            //foreach (var sceneDetail in _scenes)
            //{
            //    Debug.Log($"Name: {sceneDetail.Name}, Formation: {sceneDetail.ScreenFormation}, Position: {sceneDetail.ScenePosition}");
            //}

            MyCurrentScene = Scene.Scene1;
            OffsetPlayerPositionWithinScene();
        }
        
        public void OffsetPlayerPositionWithinScene()
        {
            var sceneService = new SceneService(MyCurrentScene);
            var offset = sceneService.GetScenePosition();

            var player = GameObject.Find("PlayerAudience");
            player.transform.position = player.transform.position + offset;

            //var panels = GameObject.Find("SelectionPanels");
            //panels.transform.position = panels.transform.position + offset;
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
            //Debug.Log("AssignVideoToDisplay");
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

        private void SpawnScene(Scene scene, ScreenFormation formation)
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


            var scenePosition = screenFormationService.ScenePosition;
            //var sceneObject = Instantiate(_sceneObject, scenePosition, Quaternion.identity);

            var sceneName = $"Scene {_sceneIndex}";
            GameObject sceneObject = new GameObject(sceneName);
            //sceneObject.name = sceneName;

            // Also instantiate selection panels, audio source and lighting as part of scene object
            var selectionPanels = Instantiate(_selectionPanels, _selectionPanels.transform.position + scenePosition, Quaternion.identity);
            var sceneAudio = Instantiate(_sceneAudio, _sceneAudio.transform.position + scenePosition, Quaternion.identity);
            var sceneLights = Instantiate(_sceneLights, _sceneLights.transform.position + scenePosition, Quaternion.identity);
            
            selectionPanels.transform.SetParent(sceneObject.transform);
            sceneAudio.transform.SetParent(sceneObject.transform);
            sceneLights.transform.SetParent(sceneObject.transform);

            selectionPanels.name = $"Selection Panel";
            sceneAudio.name = $"Scene Audio";
            sceneLights.name = $"Scene Lights";

            Scenes.Add(new SceneDetail
            {
                Id = _sceneIndex,
                Scene = scene,
                Name = sceneName,
                ScreenFormation = formation,
                ScenePosition = scenePosition,
                CurrentScreens = new List<GameObject>()
            });

            GameObject screens = new GameObject("Screens");

            var currentScene = Scenes.First(s => s.Id == _sceneIndex);

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

                    screen.transform.SetParent(screens.transform);
                    screens.transform.SetParent(sceneObject.transform);

                    currentScene.CurrentScreens.Add(screen);
                //}
            }

            _sceneIndex++;
        }

        public void TweenScreens(ScreenFormation newFormation, int tweenTimeSeconds)
        {
            TweenScreens(MyCurrentScene, newFormation, tweenTimeSeconds);
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

            var thisScene = Scenes.FirstOrDefault(s => s.Scene == scene);

            if (thisScene != null)
            {
                var audioSource =
                    GameObject.Find(thisScene.Name)
                        .transform.Find("Scene Audio")
                        .GetComponent<AudioSource>();

                audioSource.Play();

                foreach (var screenPosition in thisFormation)
                {
                    var screenPositionPrev = thisScene.CurrentScreens[screenPosition.Id - 1];

                    var vector3To = screenPosition.Vector3;
                    vector3To.y += _floorAdjust;
                    
                    screenPositionPrev.transform.DOMove(vector3To, tweenTimeSeconds).SetEase(Ease.Linear);
                    screenPositionPrev.transform.DORotate(new Vector3(0, screenPosition.Rotation, 0), 3)
                        .SetEase(Ease.Linear);
                }

            }
        }
    }
}
