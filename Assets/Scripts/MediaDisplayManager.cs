using System;
using System.Collections;
using Assets.Scripts.Models;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Services;
using DG.Tweening;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts
{
    public class MediaDisplayManager: RealtimeComponent<MediaScreenDisplayModel>
    {
        public static MediaDisplayManager instance;

        private int _sceneIndex;

        private int _lastSelectedVideoId;
        private int _lastSelectedStreamId;

        private int _lastSelectedDisplayId;
        private MediaType _lastSelectedMediaType;

        private float _floorAdjust = 1.25f;

        [SerializeField] private VideoClip[] _videoClips = new VideoClip[5];
        [SerializeField] private Transform _selectButton;
        [SerializeField] private GameObject _screen;
        [SerializeField] private GameObject _screenVariant;
        [SerializeField] private AudioSource _sceneAudio;
        [SerializeField] private GameObject _sceneLights;
        [SerializeField] private GameObject _selectionPanels;
        [SerializeField] private Text _lobbyStatusInfoText;
        [SerializeField] private GameObject _startButton;
        [SerializeField] private Text _debugText;

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
            _startButton.SetActive(false);
            StartCoroutine(AwaitVideosFromApiBeforeStart());
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

            yield return StartCoroutine(DownloadVideoFiles(Videos));

            Scenes = new List<SceneDetail>();
            _sceneIndex = 1;

            SpawnScene(Scene.Scene1, ScreenFormation.LargeSquare);
            SpawnScene(Scene.Scene2, ScreenFormation.ShortRectangle);
            SpawnScene(Scene.Scene3, ScreenFormation.Circle);
            SpawnScene(Scene.Scene4, ScreenFormation.Cross);
            SpawnScene(Scene.Scene5, ScreenFormation.LargeSquare);
            SpawnScene(Scene.Scene6, ScreenFormation.LongRectangle);
            SpawnScene(Scene.Scene7, ScreenFormation.LargeStar);
            SpawnScene(Scene.Scene8, ScreenFormation.Triangle);

            CreateStreamSelectButtons();

            MyCurrentScene = Scene.Scene1;
            //OffsetPlayerPositionWithinScene();

        }

        private IEnumerator DownloadVideoFiles(List<MediaDetail> mediaDetails)
        {
            Debug.Log("Downloading video files: -");
            _lobbyStatusInfoText.text = "Downloading video files: -\r\n";
            _debugText.text = "Saving video file to: ";

            foreach (var mediaDetail in mediaDetails.Where(m => m.Source == Source.Url))
            {
                string url = mediaDetail.Url;

                string savePath = "";
                if (Application.platform == RuntimePlatform.Android)
                {
                    string rootPath = Application.persistentDataPath.Substring(0, Application.persistentDataPath.IndexOf("Android", StringComparison.Ordinal));
                    savePath = Path.Combine(Path.Combine(rootPath, "Android/Data/_Videos"), mediaDetail.Filename);
                }
                else
                {
                    Debug.Log($"Saving video file to: {savePath}");
                    savePath = $"{Application.persistentDataPath}/{mediaDetail.Filename}";
                }

                _debugText.text += $"{savePath}";
                _lobbyStatusInfoText.text += $"{mediaDetail.Filename}\n";

                using (UnityWebRequest www = UnityWebRequest.Get(url))
                {
                    yield return www.Send();
                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        System.IO.File.WriteAllBytes(savePath, www.downloadHandler.data);
                        mediaDetail.LocalPath = savePath;
                    }

                    if (File.Exists(savePath))
                        _debugText.text += ": Saved!\n";
                    else
                        _debugText.text += ": Not saved!\n";
                }
            }

            _lobbyStatusInfoText.text += "Finished.\r\n";
            _startButton.SetActive(true);
        }

        public void OffsetPlayerPositionWithinScene()
        {
            var sceneService = new SceneService(MyCurrentScene);
            var offset = sceneService.GetScenePosition();
            var player = GameObject.Find("Player");
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
            //_displayVideo = new Dictionary<int, MediaDetail>();

            //foreach (var video in Videos)
            //{
            //    _displayVideo.Add(video.Id, video);
            //}
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

        public IEnumerator GetVideosFromApi()
        {
            // Get external video URLs from database
            var apiService = new ApiService();
            var videosFromApi = apiService.VideosGet();

            yield return new WaitUntil(() => videosFromApi.Count > 0);

            Debug.Log($"GetVideosFromApi - done: {videosFromApi.Count}");
            Videos.AddRange(videosFromApi);
        }

        private void MediaAssignedToDisplay(RealtimeArray<MediaScreenDisplayStateModel> mediaScreenDisplayStates, MediaScreenDisplayStateModel mediaScreenDisplayState, bool remote)
        {
            AssignMediaToDisplay();
        }

        protected override void OnRealtimeModelReplaced(MediaScreenDisplayModel previousModel, MediaScreenDisplayModel currentModel)
        {
            // Clear Mesh
            //_mesh.ClearRibbon();

            // TODO: Clear screens

            Debug.Log("OnRealtimeModelReplaced");

            if (previousModel != null)
            {
                Debug.Log("previousModel != null");

                // Unregister from events
                previousModel.mediaScreenDisplayStates.modelAdded -= MediaAssignedToDisplay;
            }


            if (currentModel != null)
            {
                Debug.Log($"currentModel != null. Models: {currentModel.mediaScreenDisplayStates.Count}");
                AssignMediaToDisplaysFromArray();

                // Let us know when a new screen has changed 
                currentModel.mediaScreenDisplayStates.modelAdded += MediaAssignedToDisplay;
            }
        }
        
        public void AssignMediaToDisplay()
        {
            switch (_lastSelectedMediaType)
            {
                case MediaType.VideoClip:
                    Debug.Log($"Assign video clip {_lastSelectedVideoId} to display {_lastSelectedDisplayId}");
                    AssignVideoToDisplay(_lastSelectedVideoId, _lastSelectedDisplayId);
                    break;

                case MediaType.VideoStream:
                    Debug.Log($"Assign video stream {_lastSelectedStreamId} to display {_lastSelectedDisplayId}");
                    AssignStreamToDisplay(_lastSelectedStreamId, _lastSelectedDisplayId);
                    break;
            }
        }

        public void AssignMediaToDisplaysFromArray()
        {
            foreach (var mediaInfo in model.mediaScreenDisplayStates)
            {
                Debug.Log($"AssignMediaToDisplaysFromArray. mediaInfo: {mediaInfo.screenDisplayId}");

                switch (mediaInfo.mediaTypeId)
                {
                    case (int) MediaType.VideoClip:
                        Debug.Log($"Assign video clip {mediaInfo.mediaId} to display {mediaInfo.screenDisplayId}");
                        AssignVideoToDisplay(mediaInfo.mediaId, mediaInfo.screenDisplayId);
                        break;

                    case (int) MediaType.VideoStream:
                        Debug.Log($"Assign video stream {mediaInfo.mediaId} to display {mediaInfo.screenDisplayId}");
                        AssignStreamToDisplay(mediaInfo.mediaId, mediaInfo.screenDisplayId);
                        break;
                }
            }
        }


        public void StoreScreenDisplayState()
        {
            MediaScreenDisplayStateModel mediaScreenDisplayState = new MediaScreenDisplayStateModel
            {
                mediaTypeId = (int) _lastSelectedMediaType,
                mediaId = _lastSelectedMediaType == MediaType.VideoClip
                    ? _lastSelectedVideoId
                    : _lastSelectedStreamId,
                screenDisplayId = _lastSelectedDisplayId
            };

            model.mediaScreenDisplayStates.Add(mediaScreenDisplayState);
            //Debug.Log($"mediaScreenDisplayStates: {model.mediaScreenDisplayStates.Count}");
        }

        private void AssignVideoToDisplay(int videoId, int displayId)
        {
            //Debug.Log("AssignVideoToDisplay");
            //Debug.Log($"videoId: {videoId}");
            //Debug.Log($"displayId: {displayId}");

            var displaySuffix = "Wide";

            var canvasDisplayName = $"CanvasDisplay{displaySuffix}";
            var videoDisplayName = $"VideoDisplay{displaySuffix}";

            var sceneId = int.Parse(displayId.ToString().Substring(0, 1));
            var localDisplayId = int.Parse(displayId.ToString().Substring(1, 2));

            if (videoId > 0 && displayId > 0) // && _displayVideo[localDisplayId].Id != videoId)
            {
                Debug.Log($"Assign videoId: {videoId}");
                var video = Videos.FirstOrDefault(v => v.Id == videoId);
                //video.Show = true;

                var screensContainerName = $"Screens {sceneId}";
                var screenName = $"Screen {displayId}";
                var screenVariantName = $"Screen Variant {displayId}";

                //Debug.Log($"screensContainerName: {screensContainerName}");
                //Debug.Log($"screenName: {screenName}");
                //Debug.Log($"screenVariantName: {screenVariantName}");

                // Using _displayVideo should be necessary only for URL based content
                //_displayVideo[localDisplayId] = video;

                var sceneName = Scenes.First(s => s.Id == sceneId).Name;
                var scene = GameObject.Find(sceneName);
                var screensContainer = scene.transform.Find(screensContainerName);

                var screenObject = screensContainer.transform.Find(screenName);
                if (screenObject == null) screenObject = screensContainer.transform.Find(screenVariantName);

                var thisVideoClip = Videos.First(v => v.Id == videoId);

                Debug.Log($"Show video '{thisVideoClip.Title}' on display {screenObject.name}");

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

                if (thisVideoClip.Source == Source.Url)
                {
                    // Video clip from Url
                    Debug.Log("URL video clip");

                    //videoPlayer.errorReceived += VideoPlayer_errorReceived;

                    //string path = "";
                    //if (Application.platform == RuntimePlatform.Android)
                    //{
                    //    string rootPath = Application.persistentDataPath.Substring(0, Application.persistentDataPath.IndexOf("Android", StringComparison.Ordinal));
                    //    path = Path.Combine(Path.Combine(rootPath, "Android/Data/_Videos"), thisVideoClip.Filename);
                    //}

                    // TODO - debuging in headset
                    //if (File.Exists(path))
                    //    _debugText.text += "\nFile has been found!\n\n";

                    videoPlayer.source = VideoSource.Url;
                    //videoPlayer.url = thisVideoClip.Url;
                    videoPlayer.url = thisVideoClip.LocalPath;
                }
                else
                {
                    // Video clip from local storage
                    Debug.Log("Local video clip");
                    var vc = _videoClips[videoId - 1];
                    videoPlayer.clip = vc;
                }

                //Set Audio Output to AudioSource
                videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

                //Assign the Audio from Video to AudioSource to be played
                videoPlayer.EnableAudioTrack(0, true);
                videoPlayer.SetTargetAudioSource(0, audioSource);

                // TODO test to see if this speeds up or slows down video play start
                //Set video To Play then prepare Audio to prevent Buffering        
                //videoPlayer.Prepare();

                //Play Video
                videoPlayer.Play();
                //Play Sound
                audioSource.Play();
            }
        }
        //private void VideoPlayer_errorReceived(VideoPlayer source, string message)
        //{
        //    _debugText.text += message;

        //    ///// To avoid memory leaks, unsubscribe from the event
        //    ///// otherwise it could continuously send this message
        //    videoPlayer.errorReceived -= VideoPlayer_errorReceived;
        //}

        private void AssignStreamToDisplay(int streamId, int displayId)
        {
            if (streamId > 0 && displayId > 0)
            {
                var agoraUsers = AgoraController.instance.AgoraUsers;

                if (agoraUsers.Any())
                {
                    Debug.Log("agoraUsers: -");
                    foreach (var user in agoraUsers)
                    {
                        Debug.Log($" - {user.Uid} (isLocal: {user.IsLocal}, leftRoom: {user.LeftRoom}, id: {user.Id})");
                    }

                    var agoraUser = agoraUsers.FirstOrDefault(u => u.Id == streamId);

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
                            agoraUser.DisplayId = displayId;
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
                //case ScreenFormation.SmallStar:
                //    thisFormation = screenFormationService.SmallStar();
                //    break;
                case ScreenFormation.LargeStar:
                    thisFormation = screenFormationService.LargeStar();
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
            var sceneObject = GameObject.Find(sceneName);

            if (sceneObject == null)
            {
                //Debug.Log($"{sceneName} not found");
                sceneObject = new GameObject(sceneName);
            }
            

            // Instantiate selection panels, audio source and lighting as part of scene object
            
            var selectionPanelsTrans = sceneObject.transform.Find($"Selection Panel {_sceneIndex}");
            if (selectionPanelsTrans == null)
            {
                GameObject selectionPanels = Instantiate(_selectionPanels, _selectionPanels.transform.position + scenePosition, Quaternion.identity);
                selectionPanels.transform.SetParent(sceneObject.transform);
                selectionPanels.name = $"Selection Panel {_sceneIndex}";

                Text indicator = selectionPanels.transform.Find("SceneSelectorView/Canvas/SceneText").GetComponent<Text>();
                if (indicator != null)
                {
                    indicator.text = sceneName;
                }

                foreach (Transform child in selectionPanels.transform)
                {
                    Debug.Log($"In selection panel: {child.name}");
                }

                //var indicator = texts.FirstOrDefault(t => t.name == "SceneIndicator");
                //if (indicator != null) indicator.text = sceneName;
            }

            var sceneAudioTrans = sceneObject.transform.Find($"Scene Audio {_sceneIndex}");
            if (sceneAudioTrans == null)
            {
                AudioSource sceneAudio = Instantiate(_sceneAudio, _sceneAudio.transform.position + scenePosition, Quaternion.identity);
                sceneAudio.transform.SetParent(sceneObject.transform);
                sceneAudio.name = $"Scene Audio {_sceneIndex}";
            }

            var sceneLightsTrans = sceneObject.transform.Find($"Scene Lights {_sceneIndex}");
            if (sceneLightsTrans == null)
            {
                GameObject sceneLights = Instantiate(_sceneLights, _sceneLights.transform.position + scenePosition, Quaternion.identity);
                sceneLights.transform.SetParent(sceneObject.transform);
                sceneLights.name = $"Scene Lights {_sceneIndex}";
            }
            

            Scenes.Add(new SceneDetail
            {
                Id = _sceneIndex,
                Scene = scene,
                Name = sceneName,
                ScreenFormation = formation,
                ScenePosition = scenePosition,
                CurrentScreens = new List<GameObject>()
            });

            GameObject screensObject = GameObject.Find($"Screens {_sceneIndex}");

            if (screensObject == null)
            {
                //Debug.Log($"Screens {_sceneIndex} not found");
                screensObject = new GameObject($"Screens {_sceneIndex}");
                screensObject.transform.SetParent(sceneObject.transform);
            }
            
            var currentScene = Scenes.First(s => s.Id == _sceneIndex);

            foreach (var screenPosition in thisFormation)
            {
                //if (!screenPosition.Hide)
                //{
                var vector3 = screenPosition.Vector3;
                vector3.y += _floorAdjust;

                GameObject screen;

                var screenId = _sceneIndex * 100 + screenPosition.Id;
                GameObject thisScreen;
                string screenName;

                if (screenPosition.Id % 2 != 0)
                {
                    screenName = $"Screen {screenId}";
                    thisScreen = _screen;
                }
                else
                {
                    screenName = $"Screen Variant {screenId}";
                    thisScreen = _screenVariant;
                }

                screen = GameObject.Find(screenName);

                if (screen == null)
                {
                    screen = Instantiate(thisScreen, vector3, Quaternion.identity);
                    //screen = Realtime.Instantiate(screenName, vector3, Quaternion.identity);
                    screen.transform.Rotate(0, screenPosition.Rotation, 0);
                    screen.transform.SetParent(screensObject.transform);
                }
                //else
                //{
                //    Debug.Log($"{screenName} exists");
                //}

                screen.name = screenName;

                var screenNumber = screen.GetComponentInChildren<Text>();
                screenNumber.text = screenPosition.Id.ToString();

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
                //case ScreenFormation.SmallStar:
                //    thisFormation = screenFormationService.SmallStar();
                //    break;
                case ScreenFormation.LargeStar:
                    thisFormation = screenFormationService.LargeStar();
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
                        .transform.Find($"Scene Audio {thisScene.Id}")
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
