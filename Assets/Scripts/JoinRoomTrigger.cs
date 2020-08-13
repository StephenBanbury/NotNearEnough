using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("JoinRoomTrigger");
            RoomController.instance.JoinRoom();
        }
    }
}
