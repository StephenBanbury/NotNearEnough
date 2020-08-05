using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

namespace Assets.Scripts
{
    public class DisplaySelectSync : RealtimeComponent
    {
        private DisplaySelectSyncModel _model;
        private DisplaySelect _displaySelect;

        void Start()
        {
            _displaySelect = GetComponent<DisplaySelect>();
        }

        private DisplaySelectSyncModel model
        {
            set
            {
                if (_model != null)
                {
                    // Unregister from events
                    _model.displayIdDidChange -= DisplayIdDidChange;
                }

                // Store the model
                _model = value;

                if (_model != null)
                {
                    // Update the display id to match the new model
                    UpdateDisplayId();

                    // Register for events so we'll know if the value changes later
                    _model.displayIdDidChange += DisplayIdDidChange;
                }
            }
        }

        private void DisplayIdDidChange(DisplaySelectSyncModel model, int value)
        {
            // Update the display id
            UpdateDisplayId();
        }

        private void UpdateDisplayId()
        {
            // Get the value from the model and set it on the sliding scale
            // Debug.Log($"_displaySelectDisplay exists: {_displaySelectDisplay != null}");
            _displaySelect.SetDisplayId(_model.displayId);
        }

        public void SetId(int id)
        {
            // Set the value on the model
            // This will fire the valueChanged event on the model, which will update the value for both the local player and all remote players
            _model.displayId = id;
        }
    }
}