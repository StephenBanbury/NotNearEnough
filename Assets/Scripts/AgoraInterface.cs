using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using agora_gaming_rtc;
using System.Collections.Generic;
using Assets.Scripts.Models;
using Object = UnityEngine.Object; // not sure how/why this appeared when it was not in original script
using Random = UnityEngine.Random; // not sure how/why this appeared when it was not in original script

public class AgoraInterface
{
    // instance of agora engine
    private IRtcEngine mRtcEngine;
    //private uint _localPlayerUid;

    private List<AgoraUser> _joinedUsers = new List<AgoraUser>();

    public List<AgoraUser> AgoraUsers => _joinedUsers;

    // load agora engine
    public void LoadEngine(string appId)
    {
        // start sdk
        Debug.Log("initializeEngine");

        if (mRtcEngine != null)
        {
            Debug.Log("Engine exists. Please unload it first!");
            return;
        }

        // init engine
        mRtcEngine = IRtcEngine.GetEngine(appId);

        // enable log
        mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR | LOG_FILTER.CRITICAL);
    }

    public bool Join(string channel)
    {
        Debug.Log($"In Join (channel = {channel})");

        if (mRtcEngine == null)
            return false;

        // set callbacks (optional)
        mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccess;
        mRtcEngine.OnUserJoined = OnUserJoined;
        mRtcEngine.OnUserOffline = OnUserOffline;

        // enable video
        mRtcEngine.EnableVideo();
        // allow camera output callback
        mRtcEngine.EnableVideoObserver();

        // join channel
        mRtcEngine.JoinChannel(channel, null, 0);

        // Optional: if a data stream is required, here is a good place to create it
        int streamID = mRtcEngine.CreateDataStream(true, true);
        Debug.Log("initializeEngine done, data stream id = " + streamID);

        return true;
    }

    public string GetSdkVersion()
    {
        string ver = IRtcEngine.GetSdkVersion();
        if (ver == "2.9.1.45")
        {
            ver = "2.9.2";  // A conversion for the current internal version#
        }
        else
        {
            if (ver == "2.9.1.46")
            {
                ver = "2.9.2.2";  // A conversion for the current internal version#
            }
        }
        return ver;
    }

    public void Leave()
    {
        Debug.Log("In Leave");

        if (mRtcEngine == null)
            return;

        // leave channel
        mRtcEngine.LeaveChannel();
        // deregister video frame observers in native-c code
        mRtcEngine.DisableVideoObserver();
    }

    // unload agora engine
    public void UnloadEngine()
    {
        Debug.Log("In UnloadEngine");

        // delete
        if (mRtcEngine != null)
        {
            IRtcEngine.Destroy();  // Place this call in ApplicationQuit
            mRtcEngine = null;
        }
    }


    public void EnableVideo(bool pauseVideo)
    {
        if (mRtcEngine != null)
        {
            if (!pauseVideo)
            {
                mRtcEngine.EnableVideo();
            }
            else
            {
                mRtcEngine.DisableVideo();
            }
        }
    }

    // accessing GameObject in Scnene1
    // set video transform delegate for statically created GameObject
    public void OnSceneLoaded()
    {
        Debug.Log("OnSceneLoaded");

        // Attach the SDK Script VideoSurface for video rendering
        //GameObject quad = GameObject.Find("Quad");
        //if (ReferenceEquals(quad, null))
        //{
        //    Debug.Log("BBBB: failed to find Quad");
        //    return;
        //}
        //else
        //{
        //    quad.AddComponent<VideoSurface>();
        //}
    }


    // implement engine callbacks
    private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("OnJoinChannelSuccess: uid = " + uid);

        _joinedUsers.Add(new AgoraUser
        {
            Uid = uid,
            IsLocal = true,
            DateJoined = DateTime.UtcNow
        });

        //GameObject textVersionGameObject = GameObject.Find("VersionText");
        //textVersionGameObject.GetComponent<Text>().text = "SDK Version : " + GetSdkVersion();
    }

    // When a remote user joins, this delegate will be called. 
    // Typically create a GameObject to render video on it
    private void OnUserJoined(uint uid, int elapsed)
    {
        // this is called in main thread

        Debug.Log("onUserJoined: uid = " + uid + " elapsed = " + elapsed);

        var userAlreadyJoined = _joinedUsers.Any(u => u.Uid == uid); // || _joinedUsers.Count == 1;

        if (userAlreadyJoined)
        {
            Debug.Log($"User already joined");
        }
        else
        {
            // TODO: displayId to be set by local user 
            var displayId = _joinedUsers.Count + 1;

            var agoraUser = new AgoraUser
            {
                Uid = uid,
                DateJoined = DateTime.UtcNow,
                DisplayId = displayId
            };

            _joinedUsers.Add(agoraUser);

            Debug.Log($"Number joined: {_joinedUsers.Count}");
            Debug.Log("Joined users: -");
            foreach (var user in _joinedUsers)
            {
                Debug.Log($" - {user.Uid} (isLocal: {user.IsLocal}");
            }

            //// find a game object to render video stream from 'uid'
            //GameObject go = GameObject.Find(uid.ToString());

            //if (!ReferenceEquals(go, null))
            //{
            //    return; // reuse
            //}

            // Create a GameObject and assign to this new user
            VideoSurface videoSurface = MakeImageSurface(agoraUser);

            if (!ReferenceEquals(videoSurface, null))
            {
                // configure videoSurface
                videoSurface.SetForUser(uid);
                videoSurface.SetEnable(true);
                videoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
                videoSurface.SetGameFps(30);
            }
        }
    }

    public VideoSurface MakeImageSurface(AgoraUser user)
    {
        // find a game object to render video stream from 'uid'

        var goName = user.Uid.ToString();
        var displayId = user.DisplayId;

        GameObject go = GameObject.Find(goName);

        if (!ReferenceEquals(go, null))
        {
            return null; 
        }
        
        go = new GameObject { name = goName };

        // To be rendered onto
        go.AddComponent<RawImage>();

        // make the object draggable
        //go.AddComponent<UIElementDragger>();


        var screensContainer = GameObject.Find("Screens");
        var screenObject = screensContainer.transform.Find($"StreamingScreen{displayId}");

        var videoDisplay = screenObject.transform.Find("VideoDisplay");
        var canvasDisplay = screenObject.transform.Find("CanvasDisplay");

        videoDisplay.gameObject.SetActive(false);
        canvasDisplay.gameObject.SetActive(true);


        //var displayName = $"StreamingScreen{displayId}";
        //var displayParent = GameObject.Find(displayName);
        //var displayCanvas = displayParent.transform.Find("CanvasDisplay");
        
        if (canvasDisplay != null)
        {
            foreach (GameObject child in canvasDisplay.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            go.transform.parent = canvasDisplay.transform;
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

    // When remote user is offline, this delegate will be called. Typically
    // delete the GameObject for this user
    private void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        // remove video stream
        Debug.Log("onUserOffline: uid = " + uid + " reason = " + reason);
        // this is called in main thread
        GameObject go = GameObject.Find(uid.ToString());
        if (!ReferenceEquals(go, null))
        {
            Object.Destroy(go);
        }
    }
}
