using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class FormationSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _formationId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                var gameManager = GameObject.Find("GameManager");
                var scenes = MediaDisplayManager.instance.Scenes;
                var sceneName = GetCurrentSceneFromParent();
                var scene = scenes.First(s => s.Name == sceneName).Scene;

                var formationSelect = gameManager.GetComponent<FormationSelect>();

                formationSelect.SetFormationId(scene, _formationId, 10);

                //formationSelect.KeepInSync();
            }
        }

        private string GetCurrentSceneFromParent()
        {
            var parentScene = transform.parent.parent.parent.gameObject;
            return parentScene.name;
        }

    }
}