using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class VideoSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _videoId;

        //private VideoSelect _videoSelectDisplay;

        //void Start()
        //{
        //    _videoSelectDisplay = gameObject.GetComponentInParent<VideoSelect>();
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                Debug.Log($"Video select:{_videoId}");

                var scenes = MediaDisplayManager.instance.Scenes;
                var sceneName = GetCurrentSceneFromParent();
                var scene = scenes.First(s => s.Name == sceneName).Scene;

                var gameManager = GameObject.Find("GameManager");

                var videoSelect = gameManager.GetComponent<VideoSelect>();
                videoSelect.SetVideoId(_videoId);
                videoSelect.KeepInSync();

                //_videoSelectDisplay.SetVideoId(_videoId);
                //_videoSelectDisplay.KeepInSync();
            }
        }

        private string GetCurrentSceneFromParent()
        {
            var parentScene = transform.parent.parent.parent.gameObject;
            return parentScene.name;
        }
    }
}