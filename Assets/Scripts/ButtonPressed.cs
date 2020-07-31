using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] private int _screenId;
    //[SerializeField] private Text _screenIdText;

    //private int _id;
    //private int _previousId;

    private ScreenSelectSync _screenSelectSync;
    private ScreenSelectDisplay _screenSelectDisplay;

    //public void SetScreenId(int id)
    //{
    //    Debug.Log($"SetScreenId: {id}");
    //    _id = id;
    //    _screenIdText.text = _id.ToString();
    //}

    void Start()
    {
        // Get a reference to the screen select sync component
        // _screenSelectSync = GetComponent<ScreenSelectSync>();

        Debug.Log("In ScreenSelectSync Start");

        _screenSelectSync = gameObject.GetComponentInParent<ScreenSelectSync>();
        _screenSelectDisplay = gameObject.GetComponentInParent<ScreenSelectDisplay>();

        if (_screenSelectSync == null)
        {
            Debug.Log("Can't find _screenSelectSync");
        }
        if (_screenSelectDisplay == null)
        {
            Debug.Log("Can't find _screenSelectDisplay");
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("Hand entered!");

            //_id = _screenId;

            //_screenIdText.text = _id.ToString();
            _screenSelectDisplay.SetScreenId(_screenId);

            // If the id has changed, call SetValue on the sliding scale sync component
            //if (_id != _previousId)
            //{
            //    Debug.Log($"_id: {_id}, _previousId: {_previousId}");

            //    _screenSelectSync.SetId(_id);
            //    _previousId = _id;
            //}

            _screenSelectDisplay.KeepInSync(_screenId);
        }
    }
    
}
