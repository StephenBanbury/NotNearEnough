﻿using UnityEngine;

namespace Assets.Scripts
{
    public class TeleportManager : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                var spawnPoint = GameObject.Find("SpawnPoint1").transform;
                var capsule = GameObject.Find("Capsule").transform;
                var player = GameObject.Find("Player").transform;

                capsule.position = spawnPoint.position;
                player.position = spawnPoint.position;

                //player.SetParent(spawnPoint);
                //player.position = new Vector3(0f, 0f, 0f);
            }
        }

    }

}