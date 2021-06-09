using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StreamSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _streamIdText;
        [SerializeField] private int _streamId;

        public int StreamId { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                var scenes = MediaDisplayManager.instance.Scenes;
                var sceneName = GetCurrentSceneFromParent();
                var scene = scenes.First(s => s.Name == sceneName).Scene;

                if (MediaDisplayManager.instance.CanTransformScene.Contains(scene))
                {
                    MediaDisplayManager.instance.StreamSelect(_streamId);

                    //var gameManager = GameObject.Find("GameManager");
                    //var displayManager = gameManager.GetComponent<MediaDisplayManager>();

                    ////formationSelect.SetFormationId(scene, _formationId, 10);

                    //_streamId.KeepInSync(scene, _streamId);

                    _streamIdText.text = _streamId.ToString();

                    Debug.Log($"StreamSelectButtonPressed: {_streamId}");
                }
            }
        }

        private string GetCurrentSceneFromParent()
        {
            var parentScene = transform.parent.parent.parent.gameObject;
            return parentScene.name;
        }

    }
}