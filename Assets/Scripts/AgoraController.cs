using System;
using Assets.Scripts.Models;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using agora_gaming_rtc;
using Assets.Scripts;
using Object = UnityEngine.Object; // not sure how/why this appeared when it was not in original script
using Random = UnityEngine.Random; // not sure how/why this appeared when it was not in original script

#if (UNITY_ANDROID)
using System.Collections;
using UnityEngine.Android;
#endif

public class AgoraController : MonoBehaviour
{
    public static AgoraController instance;

    // Use this for initializations
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
    private ArrayList permissionList = new ArrayList();
#endif

    private static AgoraInterface _app = null;
    private static List<AgoraUser> _agoraUsers;
    private string _homeSceneName = "JoinRoom";
    private string _playSceneName = "MainRoom";

    [SerializeField] private string _appID = "54f15673a8fd43318b10d4e42f8dd781";
    [SerializeField] private string _roomName = "unity3d";
    [SerializeField] private Text _testText;

    public List<AgoraUser> AgoraUsers
    {
        get { return _joinedUsers; }
    }

    private List<AgoraUser> _joinedUsers = new List<AgoraUser>();

    void Awake()
    {
#if (UNITY_ANDROID)
		permissionList.Add(Permission.Microphone);         
		permissionList.Add(Permission.Camera);
#endif

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // keep this alive across scenes
        DontDestroyOnLoad(this.gameObject);
        
        JoinRoom();
    }

    void Start()
    {
        CheckAppId();
    }

    void Update()
    {
        CheckPermissions();
    }

    public void UserJoined(AgoraUser agoraUser)
    {
        // TODO check if user already has joined
        _joinedUsers.Add(agoraUser);
    }

    public void UserJoinsRoom(uint uid)
    {
        //var displayId = UserDisplayId(uid);

        //var userAlreadyJoined = displayId != -1;

        var userAlreadyJoined = _joinedUsers.Any(u => u.Uid == uid);

        if (userAlreadyJoined)
        {
            Debug.Log($"User already joined");
        }
        else
        {
            // TODO: displayId to be set by local user 
            //var displayId = _joinedUsers.Count + 1;

            var newId = _joinedUsers.Max(u => u.Id) + 1;

            var agoraUser = new AgoraUser
            {
                Id = newId,
                Uid = uid,
                DateJoined = DateTime.UtcNow,
                Display = false
            };

            _joinedUsers.Add(agoraUser);

            Debug.Log($"Number joined: {_joinedUsers.Count}");
            foreach (var user in _joinedUsers)
            {
                Debug.Log($" - {user.Uid} (isLocal: {user.IsLocal}, leftRoom: {user.LeftRoom}, id: {user.Id})");
            }

            MediaDisplayManager.instance.CreateStreamSelectButtons();
        }
    }

    public void UserLeavesRoom(uint uid)
    {
        var leavingUser = _joinedUsers.FirstOrDefault(u => u.Uid == uid);
        if (leavingUser != null)
        {
            leavingUser.LeftRoom = true;

            GameObject go = GameObject.Find(uid.ToString());
            if (!ReferenceEquals(go, null))
            {
                Object.Destroy(go);
            }

            MediaDisplayManager.instance.CreateStreamSelectButtons();
        }
    }

    public void AssignStreamToDisplay(AgoraUser agoraUser)
    {
        // Create a GameObject and assign to this new user
        VideoSurface videoSurface = MakeImageSurface(agoraUser);

        if (!ReferenceEquals(videoSurface, null))
        {
            // configure videoSurface
            videoSurface.SetForUser(agoraUser.Uid);
            videoSurface.SetEnable(true);
            videoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
            videoSurface.SetGameFps(30);
        }
    }
    
    private VideoSurface MakeImageSurface(AgoraUser user)
    {

        // find a game object to render video stream from 'uid'

        var goName = user.Uid.ToString();
        var displayId = user.DisplayId;

        var displayName = $"{goName}_{displayId}";

        Debug.Log($"In MakeImageSurface. displayName: {displayName}");

        GameObject go = GameObject.Find(displayName);
        
        if (!ReferenceEquals(go, null))
        {
            return null;
        }

        go = new GameObject { name = displayName };

        // To be rendered onto
        go.AddComponent<RawImage>();

        var screensContainer = GameObject.Find("Screens");
        var screenObject = screensContainer.transform.Find($"StreamingScreen{displayId}");

        Debug.Log($"screenObject: {screenObject.name}");
        
        var videoDisplay = screenObject.transform.Find("VideoDisplay");
        var canvasDisplay = screenObject.transform.Find("CanvasDisplay");

        videoDisplay.gameObject.SetActive(false);
        canvasDisplay.gameObject.SetActive(true);

        Debug.Log($"canvasDisplay: {canvasDisplay.name}");

        if (canvasDisplay != null)
        {
            foreach (GameObject child in canvasDisplay.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            //go.transform.parent = canvasDisplay.transform;
            go.transform.SetParent(canvasDisplay.transform);
        }

        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = new Vector3(0.19f, 0.39f, 0.1f);

        // Configure videoSurface
        VideoSurface videoSurface = go.AddComponent<VideoSurface>();

        return videoSurface;
    }

    public VideoSurface MakePlaneSurface(string goName)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);

        if (go == null)
        {
            return null;
        }

        go.name = goName;
        // set up transform
        go.transform.Rotate(-90.0f, 0.0f, 0.0f);
        float yPos = Random.Range(3.0f, 5.0f);
        float xPos = Random.Range(-2.0f, 2.0f);
        go.transform.position = new Vector3(xPos, yPos, 0f);
        go.transform.localScale = new Vector3(0.25f, 0.5f, .5f);

        // configure videoSurface
        VideoSurface videoSurface = go.AddComponent<VideoSurface>();
        return videoSurface;
    }

    private int UserDisplayId(uint uid)
    {
        Debug.Log($"UserDisplay for user {uid}");

        var agoraUser = _joinedUsers.FirstOrDefault(u => u.Uid == uid);
        if (agoraUser != null)
        {
            return agoraUser.DisplayId;
        }

        return -1;
    }

    private void CheckAppId()
    {
        Debug.Assert(_appID.Length > 10, "Please fill in your AppId first on Game Controller object.");
    }

    /// <summary>
    ///   Checks for platform dependent permissions.
    /// </summary>
    private void CheckPermissions()
    {
#if (UNITY_ANDROID)
        foreach(string permission in permissionList)
        {
            if (!Permission.HasUserAuthorizedPermission(permission))
            {                 
				Permission.RequestUserPermission(permission);
			}
        }
#endif
    }

    public void JoinRoom()
    {
        Debug.Log($"JoinRoom: {_app}");

        // create app if nonexistent
        if (ReferenceEquals(_app, null))
        {
            _app = new AgoraInterface(); // create app
            _app.LoadEngine(_appID); // load engine
        }

        var joined = _app.Join(_roomName);

        if (joined)
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading; // configure GameObject after scene is loaded
            _testText.text = $"Joined {_roomName}";
        }
    }

    public void OnJoinButtonClicked()
    {
        // get parameters(channel name, channel profile, etc.)
        GameObject go = GameObject.Find("ChannelName");
        InputField field = go.GetComponent<InputField>();

        _roomName = field.text;

        // join channel 
        JoinRoom();

        // jump to next scene
        SceneManager.LoadScene(_playSceneName, LoadSceneMode.Single);
    }

    public void OnLeaveButtonClicked()
    {
        if (!ReferenceEquals(_app, null))
        {
            _app.Leave(); // leave channel
            _app.UnloadEngine(); // delete engine
            _app = null; // delete app
            SceneManager.LoadScene(_homeSceneName, LoadSceneMode.Single);
        }
        Destroy(gameObject);
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("in OnLevelFinishedLoading");

        if (scene.name != _playSceneName)
        {
            Debug.Log("Something has gone wrong: PlaySceneName != scene.name");
        }
        else
        {
            if (!ReferenceEquals(_app, null))
            {
                _app.OnSceneLoaded(); // call this after scene is loaded
            }
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
    }

    void OnApplicationPause(bool paused)
    {
        if (!ReferenceEquals(_app, null))
        {
            _app.EnableVideo(paused);
        }
    }

    void OnApplicationQuit()
    {
        if (!ReferenceEquals(_app, null))
        {
            _app.UnloadEngine();
        }
    }
}
