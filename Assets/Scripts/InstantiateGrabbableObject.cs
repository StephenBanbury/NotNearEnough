﻿using UnityEngine;
using Normal.Realtime;

public class InstantiateGrabbableObject : MonoBehaviour
{
    private Realtime _realtime;
    private RealtimeTransform _realtimeTransform;

    private void Awake()
    {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();
        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;

        //_realtimeTransform.RequestOwnership();
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        //Instantiate the CubePlayer for this client once we've successfully connected to the room
        Realtime.Instantiate("NormcoreGrabbableCube",                 // Prefab name
            position: Vector3.up,          // Start 1 meter in the air
            rotation: Quaternion.identity, // No rotation
            ownedByClient: false,   // Make sure the RealtimeView on this prefab is NOT owned by this client
            preventOwnershipTakeover: false,                // DO NOT prevent other clients from calling RequestOwnership() on the root RealtimeView.
            useInstance: realtime);           // Use the instance of Realtime that fired the didConnectToRoom event.


        //Instantiate the CubePlayer for this client once we've successfully connected to the room
        Realtime.Instantiate("NormcoreGrabbableSphere",                 // Prefab name
            position: Vector3.up,          // Start 1 meter in the air
            rotation: Quaternion.identity, // No rotation
            ownedByClient: false,   // Make sure the RealtimeView on this prefab is NOT owned by this client
            preventOwnershipTakeover: false,                // DO NOT prevent other clients from calling RequestOwnership() on the root RealtimeView.
            useInstance: realtime);           // Use the instance of Realtime that fired the didConnectToRoom event.
    }
}