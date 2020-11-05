using System.Collections;
using Assets.Scripts.Models;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using agora_gaming_rtc;
using Assets.Scripts.Enums;
using Assets.Scripts.Services;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Video;
namespace Assets.Scripts
{
    public class MediaDisplayManager: MonoBehaviour
    {
        public static MediaDisplayManager instance;

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
        public List<MediaDetail> Videos { get; private set; }

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
            StartCoroutine(AwaitVideosFromApiBeforeStart());
        }
        
        public void OffsetPlayerPositionWithinScene()
        {
            var sceneService = new SceneService(MyCurrentScene);
            var offset = sceneService.GetScenePosition();
            var player = GameObject.Find("PlayerAudience");
            player.transform.position = player.transform.position + offset;

            Debug.Log($"player.transform.position: {player.transform.position}");
            Debug.Log($"Offset: {offset}");
            Debug.Log($"New position: {player.transform.position + offset}");

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
                var yStart = 0.5f;
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

        private void GetLocalVideosDetails()
        {
            // Getting videos from the service will be necessary for URL based content
            // It is not necessary at the moment as we are using local content.
            // TODO: Streaming will probably be another thing altogether!

            Debug.Log("Get Videos from local storage");

            var videoService = new VideoService();

            Videos = videoService.GetLocalVideos();

            // TODO: We may not ever need _displayVideo
            _displayVideo = new Dictionary<int, MediaDetail>();

            foreach (var video in Videos)
            {
                _displayVideo.Add(video.Id, video);
            }
        }

        private void GetVideoLinksFromTextFile()
        {
            // Get external video URLs from text file
            Debug.Log("Get Videos from text file");

            var textLines = GetVideosExternal.GetFromTextFile();

            var i = 1;
            foreach (var textLine in textLines)
            {
                var video = new MediaDetail
                {
                    Id = i,
                    Title = $"Video {Videos.Count + 1}",
                    MediaType = MediaType.VideoClip,
                    Source = Source.Url,
                    Url = textLine
                };

                Videos.Add(video);

                i++;
            }
        }

        private IEnumerator AwaitVideosFromApiBeforeStart()
        {
            Videos = new List<MediaDetail>();

            GetLocalVideosDetails();
            //GetVideoLinksFromTextFile();
            yield return StartCoroutine(GetVideosFromApi());

            Debug.Log(
                $"Number of videos: " +
                $"Local={Videos.Count(v => v.Source == Source.LocalFile)}; " +
                $"External={Videos.Count(v => v.Source == Source.Url)}");


            Scenes = new List<SceneDetail>();
            _sceneIndex = 1;

            SpawnScene(Scene.Scene1, ScreenFormation.LargeSquare);
            //SpawnScene(Scene.Scene2, ScreenFormation.ShortRectangle);
            //SpawnScene(Scene.Scene3, ScreenFormation.Circle);
            //SpawnScene(Scene.Scene4, ScreenFormation.Cross);
            //SpawnScene(Scene.Scene5, ScreenFormation.SmallSquare);
            //SpawnScene(Scene.Scene6, ScreenFormation.LongRectangle);
            //SpawnScene(Scene.Scene7, ScreenFormation.Star);
            //SpawnScene(Scene.Scene8, ScreenFormation.Triangle);

            //Debug.Log("Scenes: -");
            //foreach (var sceneDetail in Scenes)
            //{
            //    Debug.Log($"Name: {sceneDetail.Name}, Formation: {sceneDetail.ScreenFormation}, Position: {sceneDetail.ScenePosition}");
            //    //foreach (var screen in sceneDetail.CurrentScreens)
            //    //{
            //    //    Debug.Log($"Screen: {screen.id}");
            //    //}
            //}

            CreateStreamSelectButtons();

            MyCurrentScene = Scene.Scene1;
            //OffsetPlayerPositionWithinScene();

        }

        public IEnumerator GetVideosFromApi()
        {
            // Get external video URLs from database

            var apiService = new ApiService();
            var videosFromApi = apiService.VideosGet();

            yield return new WaitUntil(() => videosFromApi.Count > 0);

            Debug.Log($"GetVideosFromApi - done: {videosFromApi.Count}");

            Videos.AddRange(videosFromApi);
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
            //Debug.Log($"_lastSelectedVideoId: {_lastSelectedVideoId}");
            //Debug.Log($"_lastSelectedDisplayId: {_lastSelectedDisplayId}");

            var displaySuffix = "Wide";

            var canvasDisplayName = $"CanvasDisplay{displaySuffix}";
            var videoDisplayName = $"VideoDisplay{displaySuffix}";

            var sceneId = int.Parse(_lastSelectedDisplayId.ToString().Substring(0, 1));
            var localDisplayId = int.Parse(_lastSelectedDisplayId.ToString().Substring(1, 2));

            if (_lastSelectedVideoId > 0 && _lastSelectedDisplayId > 0) // && _displayVideo[localDisplayId].Id != _lastSelectedVideoId)
            {
                Debug.Log($"Assign videoId: {_lastSelectedVideoId}");
                var video = Videos.FirstOrDefault(v => v.Id == _lastSelectedVideoId);
                video.Show = true;

                var screensContainerName = "Screens";
                var screenName = $"Screen {_lastSelectedDisplayId}";
                var screenVariantName = $"Screen Variant {_lastSelectedDisplayId}";

                //Debug.Log($"screensContainerName: {screensContainerName}");
                //Debug.Log($"screenName: {screenName}");
                //Debug.Log($"screenVariantName: {screenVariantName}");

                // Using _displayVideo should be necessary only for URL based content
                _displayVideo[localDisplayId] = video;

                var sceneName = Scenes.First(s => s.Id == sceneId).Name;
                var scene = GameObject.Find(sceneName);
                var screensContainer = scene.transform.Find(screensContainerName);

                var screenObject = screensContainer.transform.Find(screenName);
                if (screenObject == null) screenObject = screensContainer.transform.Find(screenVariantName);

                Debug.Log($"Show video '{_displayVideo[localDisplayId].Title}' on display {screenObject.name}");

                var videoDisplay = screenObject.transform.Find(videoDisplayName);
                var canvasDisplay = screenObject.transform.Find(canvasDisplayName);

                videoDisplay.gameObject.SetActive(true);
                canvasDisplay.gameObject.SetActive(false);

                var videoPlayer = videoDisplay.GetComponentInChildren<VideoPlayer>();

                //Add AudioSource
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();

                //Disable Play on Awake for both Video and Audio
                videoPlayer.playOnAwake = false;
                audioSource.playOnAwake = false;
                audioSource.Pause();

                var thisVideoClip = Videos.First(v => v.Id == _lastSelectedVideoId);

                // Video clip from Url
                if (thisVideoClip.Source == Source.Url)
                {
                    Debug.Log("URL video clip");
                    videoPlayer.source = VideoSource.Url;
                    videoPlayer.url = thisVideoClip.Url;
                }
                else
                {
                    Debug.Log("Local video clip");
                    var vc = _videoClips[_lastSelectedVideoId - 1];
                    videoPlayer.clip = vc;
                }

                //Set Audio Output to AudioSource
                videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

                //Assign the Audio from Video to AudioSource to be played
                videoPlayer.EnableAudioTrack(0, true);
                videoPlayer.SetTargetAudioSource(0, audioSource);

                //Set video To Play then prepare Audio to prevent Buffering        
                videoPlayer.Prepare();

                //Play Video
                videoPlayer.Play();
                //Play Sound
                audioSource.Play();
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

            /*
            foreach (var screenPosition in thisFormation)
            {
                //if (!screenPosition.Hide)
                //{
                var vector3 = screenPosition.Vector3;
                vector3.y += _floorAdjust;

                GameObject screen;

                var screenId = _sceneIndex * 100 + screenPosition.Id;

                if (screenPosition.Id % 2 != 0)
                {
                    screen = Instantiate(_screen, vector3, Quaternion.identity);
                    screen.name = $"Screen {screenId}";
                }
                else
                {
                    screen = Instantiate(_screenVariant, vector3, Quaternion.identity);
                    screen.name = $"Screen Variant {screenId}";
                }

                var screenNumber = screen.GetComponentInChildren<Text>();
                screenNumber.text = screenPosition.Id.ToString();

                screen.transform.Rotate(0, screenPosition.Rotation, 0);

                screen.transform.SetParent(screens.transform);
                screens.transform.SetParent(sceneObject.transform);

                currentScene.CurrentScreens.Add(screen);
                //}
            }
            */
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
