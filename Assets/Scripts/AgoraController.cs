using Assets.Scripts.Models;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

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
    [SerializeField] private string _roomName;
    [SerializeField] private Text _testText;

    public List<AgoraUser> AgoraUsers
    {
        get { return _app.AgoraUsers; }
    }

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
