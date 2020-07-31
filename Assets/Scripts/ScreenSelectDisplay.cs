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
        _screenSelectSync = gameObject.GetComponentInParent<ScreenSelectSync>();
    }

    public void SetScreenId(int id)
    {
        _id = id;
        _screenIdText.text = _id.ToString();
    }

    public void KeepInSync(int screenId)
    {
        _id = screenId;

        // If the id has changed, call SetId on the sync component
        if (_id != _previousId)
        {
            _screenSelectSync.SetId(_id);
            _previousId = _id;
        }
    }
}
