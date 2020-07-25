using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class SlidingScaleSync : RealtimeComponent
{
    private BoxEntered _boxEntered;
    private SlidingScaleSyncModel _model;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: needed?
        _boxEntered = GetComponent<BoxEntered>();
    }

    private SlidingScaleSyncModel model
    {
        set
        {
            Debug.Log($"In model setter. Value: {value}");

            if (_model != null)
            {
                // Unregister from events
                _model.valueDidChange -= ValueDidChange;
            }

            // Store the model
            _model = value;

            if (_model != null)
            {
                // Update the scale value to match the new model
                UpdateSlidingScaleValue();

                // Register for events so we'll know if the color changes later
                _model.valueDidChange += ValueDidChange;
            }
        }
    }

    private void ValueDidChange(SlidingScaleSyncModel model, float value)
    {
        // Update the scale value
        //Debug.Log("In ValueDidChange");
        UpdateSlidingScaleValue();
    }

    private void UpdateSlidingScaleValue()
    {
        // Get the value from the model and set it on the sliding scale
        //Debug.Log("In UpdateSlidingScaleValue");
        _boxEntered.SetScaleValue(_model.scaleValue);
    }

    public void SetValue(float value)
    {
        // Set the scale value on the model
        // This will fire the valueChanged event on the model, which will update the value for both the local player and all remote players
        _model.scaleValue = value;
    }
}
