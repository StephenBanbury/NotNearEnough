using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScreenSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _screenIdText;
        [SerializeField] private int _screenId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                var scenes = MediaDisplayManager.instance.Scenes;
                var sceneName = GetCurrentSceneFromParent();
                var scene = scenes.First(s => s.Name == sceneName).Scene;

                if (MediaDisplayManager.instance.CanTransformScene.Contains(scene))
                {
                    MediaDisplayManager.instance.ScreenSelect(_screenId);

                    //var gameManager = GameObject.Find("GameManager");
                    //var displayManager = gameManager.GetComponent<MediaDisplayManager>();

                    ////formationSelect.SetFormationId(scene, _formationId, 10);

                    //_videoId.KeepInSync(scene, _videoId);

                    _screenIdText.text = _screenId.ToString();

                    Debug.Log($"ScreenSelectButtonPressed: {_screenId}");
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