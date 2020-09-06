using Normal.Realtime;

namespace Assets.Scripts
{
    public class VideoSelectSync : RealtimeComponent
    {
        private VideoSelectSyncModel _model;
        private VideoSelect _videoSelectDisplay;

        void Start()
        {
            _videoSelectDisplay = GetComponent<VideoSelect>();
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
                    //Debug.Log("in VideoSelectSyncModel");
                    // Update the video id to match the new model
                    UpdateVideoId();

                    // Register for events so we'll know if the value changes later
                    _model.videoIdDidChange += VideoIdDidChange;
                }
            }
        }

        private void VideoIdDidChange(VideoSelectSyncModel model, int value)
        {
            UpdateVideoId();
        }

        private void UpdateVideoId()
        {
            // Get the value from the model, display it 
            _videoSelectDisplay.SetVideoId(_model.videoId);

            //Debug.Log($"in UpdateVideoId: {_model.videoId}");

            MediaDisplayManager.instance.SelectedVideo = _model.videoId;
        }

        public void SetId(int id)
        {
            // Set the value on the model
            // This will fire the valueChanged event on the model, which will update the value for both the local player and all remote players
            _model.videoId = id;
        }
    }
}