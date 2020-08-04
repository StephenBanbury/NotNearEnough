using UnityEngine;

public class VideoSelectButtonPressedTest : MonoBehaviour
{ 
    public int _videoId;

    //private VideoSelectSync _videoSelectSync;
    private VideoSelectDisplay _videoSelectDisplay;

    //public static VideoSelectButtonPressedTest instance;

    void Start()
    {
        // Get reference to the sync and action components
        //_videoSelectSync = gameObject.GetComponentInParent<VideoSelectSync>();
        _videoSelectDisplay = gameObject.GetComponentInParent<VideoSelectDisplay>();
    }

    public void OnPress()
    {
        _videoId = Random.Range(1, 16);

        _videoSelectDisplay.SetVideoId(_videoId);
        _videoSelectDisplay.KeepInSync(_videoId);
    }

}
