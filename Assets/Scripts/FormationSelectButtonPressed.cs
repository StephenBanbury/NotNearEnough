using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class FormationSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _formationId;

        private FormationSelect _formationSelectDisplay;

        void Start()
        {
            _formationSelectDisplay = gameObject.GetComponentInParent<FormationSelect>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                var gameManager = GameObject.Find("GameManager");
                var scenes = MediaDisplayManager.instance.Scenes;


                Debug.Log("Scenes: -");
                foreach (var sceneDetail in scenes)
                {
                    Debug.Log($"Name: {sceneDetail.Name}, Formation: {sceneDetail.ScreenFormation}, Position: {sceneDetail.ScenePosition}");
                }


                var sceneName = GetCurrentSceneFromParent();
                
                Debug.Log($"Scene name: {sceneName}");
                
                var scene = scenes.First(s => s.Name == sceneName).Scene;

                var formationSelect = gameManager.GetComponent<FormationSelect>();
                formationSelect.SetFormationId(scene, _formationId, 10);
                //formationSelect.KeepInSync();

                //_formationSelectDisplay.SetFormationId(_formationId, 10);
                //_formationSelectDisplay.KeepInSync();
            }
        }

        private string GetCurrentSceneFromParent()
        {
            var parentScene = transform.parent.parent.parent.gameObject;
            return parentScene.name;
        }

    }
}