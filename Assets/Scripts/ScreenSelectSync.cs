using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class ScreenSelectSync : RealtimeComponent
{
    private ButtonPressed _buttonPressed;
    private ScreenSelectSyncModel _model;

    void Start()
    {
        Debug.Log("In ScreenSelectSync Start");
        _buttonPressed = GetComponent<ButtonPressed>();
    }

    private ScreenSelectSyncModel model
    {
        set
        {
            Debug.Log($"In model setter. Value: {value}");

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

                // Register for events so we'll know if the color changes later
                _model.screenIdDidChange += ScreenIdDidChange;
            }
        }
    }

    private void ScreenIdDidChange(ScreenSelectSyncModel model, int value)
    {
        // Update the screen id
        Debug.Log("In ScreenIdDidChange");
        UpdateScreenId();
    }
    private void UpdateScreenId()
    {
        // Get the value from the model and set it on the sliding scale
        Debug.Log("In UpdateScreenId");
        Debug.Log($"_buttonPressed exists: {_buttonPressed != null}");

        _buttonPressed.SetScreenId(_model.screenId);
    }

    public void SetId(int id)
    {
        // Set the scale value on the model
        // This will fire the valueChanged event on the model, which will update the value for both the local player and all remote players
        _model.screenId = id;
    }
}
