using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DisplaySelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _displayId;

        private DisplaySelectSync _displaySelectSync;
        private DisplaySelect _displaySelectDisplay;

        void Start()
        {
            // Get reference to the sync and action components
            _displaySelectSync = gameObject.GetComponentInParent<DisplaySelectSync>();
            _displaySelectDisplay = gameObject.GetComponentInParent<DisplaySelect>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                _displaySelectDisplay.SetDisplayId(_displayId);
                _displaySelectDisplay.KeepInSync(_displayId);
            }
        }

    }
}