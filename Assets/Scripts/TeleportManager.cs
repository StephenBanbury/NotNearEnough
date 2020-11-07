﻿using System.Collections;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    public class TeleportManager : MonoBehaviour
    {
        [SerializeField] private int _sceneId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                StartCoroutine(DoTeleportation());
            }
        }

        private IEnumerator DoTeleportation()
        {
            string spawnPointName = $"Spawn Point {_sceneId}";
            Transform spawnPoint = GameObject.Find(spawnPointName).transform;
            Transform playerContainer = GameObject.Find("PlayerAudience").transform;
            Transform player = playerContainer.Find("Player").transform;
            var playerController = player.GetComponent<OVRPlayerController>();
            var sceneSampleController = player.GetComponent<OVRSceneSampleController>();

            //Debug.Log($"Teleporting to {spawnPointName}");
            //Debug.Log($"SpawnPoint position: {spawnPoint.position}");

            playerController.enabled = false;
            sceneSampleController.enabled = false;

            PlayerAudioManager.instance.PlayAudioClip("Teleport 3_1");

            yield return new WaitForSeconds(1.5f);

            PlayerAudioManager.instance.PlayAudioClip("Teleport 3_2");

            yield return new WaitForSeconds(1f);

            playerContainer.position = spawnPoint.position;

            playerController.enabled = true;
            sceneSampleController.enabled = true;

            MediaDisplayManager.instance.MyCurrentScene = (Scene) _sceneId;
        }
    }
    
}