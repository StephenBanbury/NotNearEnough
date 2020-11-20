using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Enums;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts
{
    public class ScreenPanelController : MonoBehaviour
    {
        private bool _triggerIsInAction;
        private int _waitForSeconds = 2;
        private ScreenFormation _currentScreenFormation;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                if (!_triggerIsInAction)
                {
                    StartCoroutine(TriggerAction(controllerName: other.name));
                }
            }
        }

        //IEnumerator TriggerAction(string controllerName)
        //{
        //    var leftHand = controllerName == "LeftHandCollider";
        //    var rightHand = controllerName == "RightHandCollider";

        //    var leftTrigger = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0;
        //    var rightTrigger = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0;

        //    //Debug.Log($"Left Trigger: {leftTrigger}");
        //    //Debug.Log($"Right Trigger: {rightTrigger}");

        //    _triggerIsInAction = true;

        //    if (leftTrigger && rightTrigger)
        //    {
        //        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
        //        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);

        //        StartCoroutine(RaiseMeshCollider());
        //    }
        //    else if (rightHand && rightTrigger)
        //    {
        //        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);

        //        ToggleVideoOn();
        //    }
        //    else if (leftHand && leftTrigger)
        //    {
        //        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);

        //        ChangeScreenFormation();
        //    }

        //    yield return new WaitForSeconds(_waitForSeconds);

        //    Debug.Log("WaitForNextTrigger done");
        //    _triggerIsInAction = false;
        //}

        IEnumerator TriggerAction(string controllerName)
        {
            var leftHand = controllerName == "LeftHandCollider";
            var rightHand = controllerName == "RightHandCollider";

            var leftTrigger = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0;
            var rightTrigger = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0;

            if ((leftHand || rightHand) && (leftTrigger || rightTrigger))
            {
                Transform parent = gameObject.transform.parent;
                int screenId = int.Parse(parent.name.Replace("Screen", "").Replace("Variant", "").Trim());

                ScreenAction nextAction = MediaDisplayManager.instance.GetNextScreenAction(screenId);
                //MediaDisplayManager.instance.SetNextScreenAction(screenId);

                _triggerIsInAction = true;

                switch (nextAction)
                {
                    case ScreenAction.ChangeVideoClip:
                        Debug.Log("ScreenAction: Change video clip");
                        //SelectRandomVideo();
                        break;
                    case ScreenAction.ChangeVideoStream:
                        Debug.Log("ScreenAction: Change video stream");
                        break;
                    case ScreenAction.ChangeFormation:
                        Debug.Log("ScreenAction: Change screen formation");
                        //ChangeScreenFormation();
                        break;
                    case ScreenAction.CreatePortal:
                        Debug.Log("ScreenAction: Create Portal");
                        break;
                }

                yield return new WaitForSeconds(_waitForSeconds);

                Debug.Log("Time has passed");

                _triggerIsInAction = false;
            }
        }

        // Raise then lower mesh collider so that player can momentarily leave the scene. 
        IEnumerator RaiseMeshCollider()
        {
            var meshCollider = gameObject.GetComponent<MeshCollider>().transform;

            var x = meshCollider.position.x;
            var y = meshCollider.position.y;
            var z = meshCollider.position.z;

            meshCollider.position = new Vector3(x, y + 20f, z);

            PlayerAudioManager.instance.PlayAudioClip("Door 3 Open");

            yield return new WaitForSeconds(3); 

            meshCollider.position = new Vector3(x, y, z);
        }

        private void ChangeScreenFormation()
        {
            string sceneName = GetCurrentSceneFromParent();
            var scenes = MediaDisplayManager.instance.Scenes;
            var scene = scenes.First(s => s.Name == sceneName).Scene;

            if (MediaDisplayManager.instance.CanTransformScene.Contains(scene))
            {
                int numberOfFormations = Enum.GetValues(typeof(ScreenFormation)).Cast<int>().Max();

                ScreenFormation randomFormation;
                do
                {
                    randomFormation = (ScreenFormation) Math.Ceiling(Random.value * numberOfFormations);
                } while (randomFormation == _currentScreenFormation || randomFormation == ScreenFormation.None);

                _currentScreenFormation = randomFormation;

                GameObject gameManager = GameObject.Find("GameManager");
                FormationSelect formationSelect = gameManager.GetComponent<FormationSelect>();

                formationSelect.SetFormationId(scene, (int) _currentScreenFormation, 10);
                formationSelect.KeepInSync();
            }
        }

        private string GetCurrentSceneFromParent()
        {
            var parentScene = transform.parent.parent.parent.gameObject;
            return parentScene.name;
        }

        private void SelectRandomVideo()
        {
            var displaySuffix = "Wide";

            var canvasDisplayName = $"CanvasDisplay{displaySuffix}";
            var videoDisplayName = $"VideoDisplay{displaySuffix}";

            var parent = gameObject.transform.parent;
            var videoDisplay = parent.Find(videoDisplayName);
            var canvasDisplay = parent.Find(canvasDisplayName);

            // For now I am going to select a random video to display. We will probably want a different action
            var videos = MediaDisplayManager.instance.Videos;

            var videoId = (int) Math.Ceiling(Random.value * videos.Count);
            var screenId = int.Parse(parent.name.Replace("Screen", "").Replace("Variant", "").Trim());

            var gameManager = GameObject.Find("GameManager");

            var videoSelect = gameManager.GetComponent<VideoSelect>();
            videoSelect.SetVideoId(videoId);
            videoSelect.KeepInSync();

            var displaySelect = gameManager.GetComponent<DisplaySelect>();
            displaySelect.SetDisplayId(screenId);
            displaySelect.KeepInSync();

            // Will use this later...
            //if (videoDisplay)
            //{
            //    Debug.Log($"{videoDisplayName} in {parent.name} active: {videoDisplay.gameObject.activeSelf}");
            //}

            //if (canvasDisplay)
            //{
            //    Debug.Log($"{canvasDisplayName} in {parent.name} active: {canvasDisplay.gameObject.activeSelf}");
            //}

        }
    }
}
