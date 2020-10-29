using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DisplaySelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _displayIdText;
        [SerializeField] private int _displayId;

        //private DisplaySelect _displaySelectDisplay;

        //void Start()
        //{
        //    _displaySelectDisplay = gameObject.GetComponentInParent<DisplaySelect>();
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                Debug.Log($"Display select:{_displayId}");

                var scenes = MediaDisplayManager.instance.Scenes;
                var sceneName = GetCurrentSceneFromParent();
                var scene = scenes.First(s => s.Name == sceneName).Scene;

                var gameManager = GameObject.Find("GameManager");
                var displaySelect = gameManager.GetComponent<DisplaySelect>();
                displaySelect.SetDisplayId(_displayId);
                displaySelect.KeepInSync();

                _displayIdText.text = _displayId.ToString();

                //_displaySelectDisplay.SetDisplayId(_displayId);
                //_displaySelectDisplay.KeepInSync();
            }
        }

        private string GetCurrentSceneFromParent()
        {
            var parentScene = transform.parent.parent.parent.gameObject;
            return parentScene.name;
        }
    }
}