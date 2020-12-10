//using System.Linq;
//using UnityEngine;
//using UnityEngine.UI;

//namespace Assets.Scripts
//{
//    public class PortalSelectButtonPressed : MonoBehaviour
//    {
//        [SerializeField] private Text _displayIdText;
//        [SerializeField] private int _portalDisplayId;

//        private void OnTriggerEnter(Collider other)
//        {
//            if (other.CompareTag("Hand"))
//            {
//                // _displayId is a composite of scene + _displayId
//                // e.g. 814 = scene 8, display 14; 208 = scene 2, display 8

//                var scenes = MediaDisplayManager.instance.Scenes;
//                var sceneName = GetCurrentSceneFromParent();
//                var sceneId = scenes.First(s => s.Name == sceneName).Id;
//                var compositeId = sceneId * 100 + _portalDisplayId;

//                Debug.Log($"Portal select displayId:{_portalDisplayId}");

//                //if (compositeId > 0)
//                    //MediaDisplayManager.instance.StoreRealtimeScreenPortalState(compositeId);

//                _displayIdText.text = _portalDisplayId.ToString();
//            }
//        }

//        private string GetCurrentSceneFromParent()
//        {
//            var parentScene = transform.parent.parent.parent.gameObject;
//            return parentScene.name;
//        }
//    }
//}