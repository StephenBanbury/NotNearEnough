using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Enums;
using Random = UnityEngine.Random;
using UnityEngine;

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

            ChangeScreenFormation();

            yield return new WaitForSeconds(_waitForSeconds);

            Debug.Log("WaitForNextTrigger done");
            _triggerIsInAction = false;
        }

        private void ChangeScreenFormation()
        {
            var numberOfFormations = Enum.GetValues(typeof(ScreenFormation)).Cast<int>().Max();
            //Debug.Log($"Number of formations: {numberOfFormations}");
            //Debug.Log($"Current screen formation: {_currentScreenFormation}");

            ScreenFormation randomFormation;
            do
            {
                randomFormation = (ScreenFormation) Math.Floor(Random.value * numberOfFormations);
                //Debug.Log($"Trying new formation: {randomFormation}");
            } while (randomFormation == _currentScreenFormation || randomFormation == ScreenFormation.None);

            _currentScreenFormation = randomFormation;
            //Debug.Log($"New screen formation: {_currentScreenFormation}");
            MediaDisplayManager.instance.TweenScreens(_currentScreenFormation);
        }
    }
}
