using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
using System.Collections;
using UnityEngine.Android;
#endif

/// <summary>
///    RoomController serves a game controller object for this application.
/// </summary>
public class RoomController : MonoBehaviour
{

    public static RoomController instance;

    // Use this for initializations
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
    private ArrayList permissionList = new ArrayList();
#endif


    static AgoraInterface app = null;
    private string HomeSceneName = "JoinRoom";
    private string PlaySceneName = "MainRoom";

    // PLEASE KEEP THIS App ID IN SAFE PLACE
    // Get your own App ID at https://dashboard.agora.io/
    [SerializeField] private string AppID = "your_appid";

    [SerializeField] private string _roomName;
    [SerializeField] private Text _testText;

    void Awake()
    {
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
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
        Debug.Assert(AppID.Length > 10, "Please fill in your AppId first on Game Controller object.");
    }

    /// <summary>
    ///   Checks for platform dependent permissions.
    /// </summary>
    private void CheckPermissions()
    {
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
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
        Debug.Log($"JoinRoom: {app}");

        // create app if nonexistent
        if (ReferenceEquals(app, null))
        {
            app = new AgoraInterface(); // create app
            app.LoadEngine(AppID); // load engine
        }

        var joined = app.Join(_roomName);

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
        SceneManager.LoadScene(PlaySceneName, LoadSceneMode.Single);
    }

    public void OnLeaveButtonClicked()
    {
        if (!ReferenceEquals(app, null))
        {
            app.Leave(); // leave channel
            app.UnloadEngine(); // delete engine
            app = null; // delete app
            SceneManager.LoadScene(HomeSceneName, LoadSceneMode.Single);
        }
        Destroy(gameObject);
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("in OnLevelFinishedLoading");

        if (scene.name != PlaySceneName)
        {
            Debug.Log("Something has gone wrong: PlaySceneName != scene.name");
        }
        else
        {
            if (!ReferenceEquals(app, null))
            {
                app.OnSceneLoaded(); // call this after scene is loaded
            }
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
    }

    void OnApplicationPause(bool paused)
    {
        if (!ReferenceEquals(app, null))
        {
            app.EnableVideo(paused);
        }
    }

    void OnApplicationQuit()
    {
        if (!ReferenceEquals(app, null))
        {
            app.UnloadEngine();
        }
    }
}
