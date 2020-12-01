using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StreamSelectButtonPressed : MonoBehaviour
    {
        public int StreamId { get; set; }

        [SerializeField] private Text _streamIdText;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                Debug.Log($"Stream button pressed: StreamId: {StreamId}");

                //var gameManager = GameObject.Find("GameManager");
                //var streamSelect = gameManager.GetComponent<StreamSelect>();
                ////streamSelect.SetStreamId(StreamId);
                //streamSelect.KeepInSync(StreamId);

                MediaDisplayManager.instance.SelectedVideo = StreamId;
                MediaDisplayManager.instance.SelectedMediaType = MediaType.VideoStream;
            }
        }
    }
}