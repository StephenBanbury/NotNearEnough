using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] private int _screenId = 1;
    [SerializeField] private Text _screenIdText;

    private int _id;
    private int _previousId;

    private ScreenSelectSync _screenSelectSync;
    
    public void SetScreenId(int id)
    {
        Debug.Log($"SetScreenId: {id}");
        _id = id;
        _screenIdText.text = _id.ToString();
    }

    void Start()
    {
        // Get a reference to the screen select sync component
        _screenSelectSync = GetComponent<ScreenSelectSync>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("Hand entered!");

            _id = _screenId;

            _screenIdText.text = _id.ToString();

            // If the id has changed, call SetValue on the sliding scale sync component
            if (_id != _previousId)
            {
                Debug.Log($"_id: {_id}, _previousId: {_previousId}");

                _screenSelectSync.SetId(_id);
                _previousId = _id;
            }
        }
    }
    
}
