using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSelectButtonPressed : MonoBehaviour
{
    [SerializeField] private int _videoId;

    private VideoSelectSync _videoSelectSync;
    private VideoSelectDisplay _videoSelectDisplay;

    void Start()
    {
        // Get reference to the sync and action components
        _videoSelectSync = gameObject.GetComponentInParent<VideoSelectSync>();
        _videoSelectDisplay = gameObject.GetComponentInParent<VideoSelectDisplay>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            _videoSelectDisplay.SetVideoId(_videoId);
            _videoSelectDisplay.KeepInSync(_videoId);
        }
    }
    
}
