using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Normal.Realtime;
using UnityEngine;

namespace Assets.Scripts
{
    public class FormationSelectSync : RealtimeComponent
    {
        private FormationSelectSyncModel _model;
        private FormationSelect _formationSelectDisplay;

        void Start()
        {
            _formationSelectDisplay = GetComponent<FormationSelect>();
        }

        private FormationSelectSyncModel model
        {
            set
            {
                //Debug.Log("In FormationSelectSyncModel");

                if (_model != null)
                {
                    // Unregister from events
                    _model.formationIdDidChange -= FormationIdDidChange;
                }

                // Store the model
                _model = value;

                if (_model != null)
                {
                    //Debug.Log("in FormationSelectSyncModel");
                    // Update the formation id to match the new model
                    UpdateFormationId();

                    // Register for events so we'll know if the value changes later
                    _model.formationIdDidChange += FormationIdDidChange;
                }
            }
        }

        private void FormationIdDidChange(FormationSelectSyncModel model, int value)
        {
            UpdateFormationId();
        }

        private void UpdateFormationId()
        {
            // Get the value from the model, display it 
            _formationSelectDisplay.SetFormationId(_model.formationId);

            //Debug.Log($"in UpdateFormationId: {_model.formationId}");

            // TODO: this may not be needed, but if it is, maybe move it to its own manager class
            //MediaDisplayManager.instance.SelectedScreenFormation = (ScreenFormation) _model.formationId;
        }

        public void SetId(int id)
        {
            // Set the value on the model
            // This will fire the valueChanged event on the model, which will update the value for both the local player and all remote players
            _model.formationId = id;
        }
    }
}