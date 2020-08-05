using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

namespace Assets.Scripts
{
    public class VideoSelectSync : RealtimeComponent
    {
        private VideoSelectSyncModel _model;
        private VideoSelectDisplay _videoSelectDisplay;

        void Start()
        {
            _videoSelectDisplay = GetComponent<VideoSelectDisplay>();
        }

        private VideoSelectSyncModel model
        {
            set
            {
                //Debug.Log("In VideoSelectSyncModel");

                if (_model != null)
                {
                    // Unregister from events
                    _model.videoIdDidChange -= VideoIdDidChange;
                }

                // Store the model
                _model = value;

                if (_model != null)
                {
                    // Update the video id to match the new model
                    UpdateVideoId();

                    // Register for events so we'll know if the value changes later
                    _model.videoIdDidChange += VideoIdDidChange;
                }
            }
        }

        private void VideoIdDidChange(VideoSelectSyncModel model, int value)
        {
            //Debug.Log("In VideoIdDidChange");

            // Update the video id
            UpdateVideoId();
        }

        private void UpdateVideoId()
        {
            //Debug.Log("In UpdateVideoId");

            // Get the value from the model and set it on the sliding scale
            // Debug.Log($"_videoSelectDisplay exists: {_videoSelectDisplay != null}");
            _videoSelectDisplay.SetVideoId(_model.videoId);
        }

        public void SetId(int id)
        {
            // Set the value on the model
            // This will fire the valueChanged event on the model, which will update the value for both the local player and all remote players
            _model.videoId = id;

            //Debug.Log($"model.videoId:{_model.videoId}");
        }
    }
}