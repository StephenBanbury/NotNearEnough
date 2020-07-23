using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxEntered : MonoBehaviour
{
    [SerializeField] private Text _scaleText;

    private float _handHeight;

    public float HandHeight { 
        get => _handHeight; 
        set => _handHeight = value;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("Hand entered!");
            Debug.Log($"Extra: { other.transform}");

            //_handHeight = other.transform.position.y;
            //Debug.Log($"_handHeight: {_handHeight}");
        }
    }

    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    private void OnTriggerStay(Collider other)
    {
        _handHeight = other.transform.position.y;
        Debug.Log($"_handHeight: {_handHeight}");
        _scaleText.text = _handHeight.ToString();
    }


}
