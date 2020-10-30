﻿using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class VideoSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _videoIdText;
        [SerializeField] private int _videoId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                Debug.Log($"Video select videoId:{_videoId}");

                var gameManager = GameObject.Find("GameManager");

                var videoSelect = gameManager.GetComponent<VideoSelect>();
                videoSelect.SetVideoId(_videoId);

                videoSelect.KeepInSync();

                _videoIdText.text = _videoId.ToString();
            }
        }

        private string GetCurrentSceneFromParent()
        {
            var parentScene = transform.parent.parent.parent.gameObject;
            return parentScene.name;
        }
    }
}