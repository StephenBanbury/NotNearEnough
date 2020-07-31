using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSelectButtonPressed : MonoBehaviour
{
    [SerializeField] private int _screenId;

    private ScreenSelectSync _screenSelectSync;
    private ScreenSelectDisplay _screenSelectDisplay;

    void Start()
    {
        // Get reference to the sync and action components
        _screenSelectSync = gameObject.GetComponentInParent<ScreenSelectSync>();
        _screenSelectDisplay = gameObject.GetComponentInParent<ScreenSelectDisplay>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            _screenSelectDisplay.SetScreenId(_screenId);
            _screenSelectDisplay.KeepInSync(_screenId);
        }
    }
    
}
