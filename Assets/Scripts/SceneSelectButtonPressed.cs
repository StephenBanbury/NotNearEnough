using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SceneSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _sceneNameText;
        [SerializeField] private int _sceneId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                MediaDisplayManager.instance.SetPlayerPositionToScene(_sceneId);
            }
        }
    }
}