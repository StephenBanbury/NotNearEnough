using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class ScreenSelectSync : RealtimeComponent
{
    private ScreenSelectSyncModel _model;
    private ScreenSelectDisplay _screenSelectDisplay;

    void Start()
    {
        _screenSelectDisplay = GetComponent<ScreenSelectDisplay>();
    }

    private ScreenSelectSyncModel model
    {
        set
        {
            if (_model != null)
            {
                // Unregister from events
                _model.screenIdDidChange -= ScreenIdDidChange;
            }

            // Store the model
            _model = value;

            if (_model != null)
            {
                // Update the screen id to match the new model
                UpdateScreenId();

                // Register for events so we'll know if the value changes later
                _model.screenIdDidChange += ScreenIdDidChange;
            }
        }
    }

    private void ScreenIdDidChange(ScreenSelectSyncModel model, int value)
    {
        // Update the screen id
        UpdateScreenId();
    }

    private void UpdateScreenId()
    {
        // Get the value from the model and set it on the sliding scale
       // Debug.Log($"_screenSelectDisplay exists: {_screenSelectDisplay != null}");
       _screenSelectDisplay.SetScreenId(_model.screenId);
    }
    
    public void SetId(int id)
    {
        // Set the value on the model
        // This will fire the valueChanged event on the model, which will update the value for both the local player and all remote players
        _model.screenId = id;
    }
}
