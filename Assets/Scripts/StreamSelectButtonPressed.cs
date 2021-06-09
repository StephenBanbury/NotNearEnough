using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StreamSelectButtonPressed : MonoBehaviour
    {
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
                    MediaDisplayManager.instance.MediaSelect(streamId: StreamId);

                    //Debug.Log($"StreamSelectButtonPressed: {StreamId}");
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