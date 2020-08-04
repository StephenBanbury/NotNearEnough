using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSelectDisplay : MonoBehaviour
{
    [SerializeField] private Text _videoIdText;

    private int _id;
    private int _previousId;

    // TODO: move this to a service class
    private Dictionary<int, string> _videos = new Dictionary<int, string>();

    private VideoSelectSync _videoSelectSync;


    void Start()
    {
        _videoSelectSync = gameObject.GetComponentInParent<VideoSelectSync>();

        // TODO: this will go once we have it all in a service
        for (int i = 1; i <= 16; i++)
        {
            _videos.Add(i, $"Video {i}");
        }
    }

    public void SetVideoId(int id)
    {
        _id = id;
        //_videoIdText.text = _id.ToString();
        _videoIdText.text = _videos[_id];
    }

    public void KeepInSync(int videoId)
    {
        _id = videoId;

        Debug.Log($"id:{_id}, previoudId:{_previousId}");

        // If the id has changed, call SetId on the sync component
        if (_id != _previousId)
        {
            _videoSelectSync.SetId(_id);
            _previousId = _id;
        }
    }
}
