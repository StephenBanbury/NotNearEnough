using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class VideoSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _videoIdText;
        [SerializeField] private int _videoId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                var scenes = MediaDisplayManager.instance.Scenes;
                var sceneName = GetCurrentSceneFromParent();
                var scene = scenes.First(s => s.Name == sceneName).Scene;

                if (MediaDisplayManager.instance.CanTransformScene.Contains(scene))
                {
                    MediaDisplayManager.instance.MediaSelect(videoId: _videoId);

                    _videoIdText.text = _videoId.ToString();

                    //Debug.Log($"VideoSelectButtonPressed: {_videoId}");
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