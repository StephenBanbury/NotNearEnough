using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSelectDisplay : MonoBehaviour
{
    [SerializeField] private Text _screenIdText;

    private int _id;
    private int _previousId;

    private ScreenSelectSync _screenSelectSync;


    void Start()
    {
        // Get a reference to the screen select sync component
        // _screenSelectSync = GetComponent<ScreenSelectSync>();

        Debug.Log("In ScreenSelectDisplay Start");

        _screenSelectSync = gameObject.GetComponentInParent<ScreenSelectSync>();

        if (_screenSelectSync == null)
        {
            Debug.Log("Can't find _screenSelectSync");
        }
    }

    public void SetScreenId(int id)
    {
        Debug.Log($"SetScreenId: {id}");
        _id = id;
        _screenIdText.text = _id.ToString();
    }

    public void KeepInSync(int screenId)
    {

        _id = screenId;

        // If the id has changed, call SetValue on the sliding scale sync component
        if (_id != _previousId)
        {
            Debug.Log($"_id: {_id}, _previousId: {_previousId}");

            _screenSelectSync.SetId(_id);
            _previousId = _id;
        }
    }
}
