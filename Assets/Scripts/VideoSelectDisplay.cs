using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSelectDisplay : MonoBehaviour
{
    [SerializeField] private Text _videoIdText;

    private int _id;
    private int _previousId;

    private VideoSelectSync _videoSelectSync;


    void Start()
    {
        _videoSelectSync = gameObject.GetComponentInParent<VideoSelectSync>();
    }

    public void SetVideoId(int id)
    {
        _id = id;
        _videoIdText.text = _id.ToString();
    }

    public void KeepInSync(int videoId)
    {
        _id = videoId;

        // If the id has changed, call SetId on the sync component
        if (_id != _previousId)
        {
            _videoSelectSync.SetId(_id);
            _previousId = _id;
        }
    }
}
