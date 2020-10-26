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
        private int _screenButtonEventId;
        private bool _triggerIsInAction;
        private int _waitForSeconds = 5;
        private ScreenFormation _currentScreenFormation;
        
        private void Start()
        {
            _screenButtonEventId = 0;
        }

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

        IEnumerator TriggerAction(string controllerName)
        {
            // Flag to prevent quickfire actions taking place 
            _triggerIsInAction = true;

            if (_screenButtonEventId == 3)
            {
                _screenButtonEventId = 1;
            }
            else
            {
                _screenButtonEventId++;
            }

            OVRInput.SetControllerVibration(0.5f, 0.5f, controllerName == "LeftHandCollider"
                ? OVRInput.Controller.LTouch
                : OVRInput.Controller.RTouch);

            // Randomly select to change formation or change video
            //var rand = Random.value * 100;
            //if (rand <= 40)
            //{
            //    ChangeScreenFormation();
            //}
            //else
            //{
            //    ToggleVideoOn();
            //}

            ChangeScreenFormation();

            yield return new WaitForSeconds(_waitForSeconds);

            Debug.Log("WaitForNextTrigger done");
            _triggerIsInAction = false;
        }

        private void ChangeScreenFormation()
        {
            var numberOfFormations = Enum.GetValues(typeof(ScreenFormation)).Cast<int>().Max();

            ScreenFormation randomFormation;
            do
            {
                randomFormation = (ScreenFormation) Math.Ceiling(Random.value * numberOfFormations);
            } while (randomFormation == _currentScreenFormation || randomFormation == ScreenFormation.None);

            _currentScreenFormation = randomFormation;

            var gameManager = GameObject.Find("GameManager");

            var formationSelect = gameManager.GetComponent<FormationSelect>();

            formationSelect.SetFormationId((int) _currentScreenFormation, 10);
            formationSelect.KeepInSync();
        }

        private void ToggleVideoOn()
        {
            var parent = gameObject.transform.parent;
            var videoDisplay = parent.Find("VideoDisplay");
            var canvasDisplay = parent.Find("CanvasDisplay");

            // For now I am going to select a random video to display. We will probably want a different action

            var videoId = (int) Math.Ceiling(Random.value * 5);
            var screenId = int.Parse(parent.name.Replace("Screen", "").Replace("Variant", "").Trim());

            var gameManager = GameObject.Find("GameManager");

            var videoSelect = gameManager.GetComponent<VideoSelect>();
            videoSelect.SetVideoId(videoId);
            videoSelect.KeepInSync();

            var displaySelect = gameManager.GetComponent<DisplaySelect>();
            displaySelect.SetDisplayId(screenId);
            displaySelect.KeepInSync();

            // Will use this later...
            if (videoDisplay)
            {
                Debug.Log($"VideoDisplay in {parent.name} active: {videoDisplay.gameObject.activeSelf}");
            }

            if (canvasDisplay)
            {
                Debug.Log($"CanvasDisplay in {parent.name} active: {canvasDisplay.gameObject.activeSelf}");
            }

        }
    }
}
