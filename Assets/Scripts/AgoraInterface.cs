using System;
using UnityEngine;
using agora_gaming_rtc;
using Assets.Scripts.Models;

namespace Assets.Scripts
{
    public class AgoraInterface
    {
        // instance of agora engine
        private IRtcEngine mRtcEngine;

        // load agora engine
        public void LoadEngine(string appId)
        {
            // start sdk
            Debug.Log("Agora InitializeEngine");

            if (mRtcEngine != null)
            {
                Debug.Log("Engine exists. Please unload it first!");
                return;
            }

            // init engine
            mRtcEngine = IRtcEngine.GetEngine(appId);

            // enable log
            mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR |
                                    LOG_FILTER.CRITICAL);
        }

        // Added by me.
        // TODO: this may be where we start when attempting to control audio
        // but it may also not be! 
        private void GetAudioDevice()
        {
            var d = "";
            var audioPlaybackDeviceManager = AudioPlaybackDeviceManager.GetInstance(mRtcEngine);
            var playebackDeviceId = audioPlaybackDeviceManager.GetCurrentPlaybackDevice(ref d);
            Debug.Log($"playbackDevice: {playebackDeviceId}");
        }

        public bool Join(string channel)
        {
            Debug.Log($"Agora Join (channel = {channel})");

            if (mRtcEngine == null)
                return false;

            // set callbacks (optional)
            mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccess;
            mRtcEngine.OnUserJoined = OnUserJoined;
            mRtcEngine.OnUserOffline = OnUserOffline;

            // Added by Me
            mRtcEngine.OnStreamMessage = OnStreamMessage;

            // enable video
            mRtcEngine.EnableVideo();
            // allow camera output callback
            mRtcEngine.EnableVideoObserver();

            // join channel
            mRtcEngine.JoinChannel(channel, null, 0);

            // Optional: if a data stream is required, here is a good place to create it
            int streamID = mRtcEngine.CreateDataStream(true, true);
            Debug.Log("Agora InitializeEngine done, data stream id = " + streamID);

            return true;
        }

        public string GetSdkVersion()
        {
            string ver = IRtcEngine.GetSdkVersion();
            if (ver == "2.9.1.45")
            {
                ver = "2.9.2"; // A conversion for the current internal version#
            }
            else
            {
                if (ver == "2.9.1.46")
                {
                    ver = "2.9.2.2"; // A conversion for the current internal version#
                }
            }

            return ver;
        }

        public void Leave()
        {
            if (mRtcEngine == null) return;

            // leave channel
            mRtcEngine.LeaveChannel();
            // deregister video frame observers in native-c code
            mRtcEngine.DisableVideoObserver();
        }

        // unload agora engine
        public void UnloadEngine()
        {
            Debug.Log("Agora UnloadEngine");

            // delete
            if (mRtcEngine != null)
            {
                IRtcEngine.Destroy(); // Place this call in ApplicationQuit
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
            Debug.Log("Agora OnSceneLoaded");
        }


        // implement engine callbacks
        private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
        {
            // TODO: not sure this is required

            Debug.Log("Agora OnJoinChannelSuccess: uid = " + uid);

            AgoraController.instance.UserJoined(new AgoraUser
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

            Debug.Log("Agora onUserJoined: uid = " + uid + " elapsed = " + elapsed);

            // Added by me. 
            // TODO: consider continuing this process. This may be where we start gaining control over our audio devices
            GetAudioDevice();

            AgoraController.instance.UserJoinsRoom(uid);

            // Optional: if a data stream is required, here is a good place to create it
            int streamID = mRtcEngine.CreateDataStream(true, true);
            Debug.Log("initializeEngine done, data stream id = " + streamID);

           // mRtcEngine.SendStreamMessage(streamID, "Hello from GAM750-6!");

        }

        void OnStreamMessage(uint userId, int streamId, string data, int length)
        {
            Debug.Log($"Agora Message from {userId}: {data}");
        }

        // When remote user is offline, this delegate will be called. Typically
        // delete the GameObject for this user
        private void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
        {
            // remove video stream
            Debug.Log("Agora onUserOffline: uid = " + uid + " reason = " + reason);

            // this is called in main thread

            //GameObject go = GameObject.Find(uid.ToString());
            //if (!ReferenceEquals(go, null))
            //{
            //    Object.Destroy(go);
            //}

            AgoraController.instance.UserLeavesRoom(uid);
        }
    }
}