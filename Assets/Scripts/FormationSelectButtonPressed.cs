using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FormationSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _formationIdText;
        [SerializeField] private int _formationId;

        //private FormationSelect _formationSelectDisplay;

        //void Start()
        //{
        //    _formationSelectDisplay = gameObject.GetComponentInParent<FormationSelect>();
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                var scenes = MediaDisplayManager.instance.Scenes;
                var sceneName = GetCurrentSceneFromParent();
                var scene = scenes.First(s => s.Name == sceneName).Scene;

                var gameManager = GameObject.Find("GameManager");
                var formationSelect = gameManager.GetComponent<FormationSelect>();

                formationSelect.SetFormationId(scene, _formationId, 10);

                formationSelect.KeepInSync();

                _formationIdText.text = _formationId.ToString();

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