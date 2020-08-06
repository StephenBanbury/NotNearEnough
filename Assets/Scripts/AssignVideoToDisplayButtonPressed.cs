using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class AssignVideoToDisplayButtonPressed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            //VideoDisplayManager.instance.AssignVideoToDisplay();
        }
    }

}
