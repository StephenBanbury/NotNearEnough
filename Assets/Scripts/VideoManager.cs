using Assets.Scripts.Models;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Services;

namespace Assets.Scripts
{
    class VideoManager: MonoBehaviour
    {
        public static VideoManager instance;

        private List<MediaDetail> _videos;
        private readonly VideoService videoService = new VideoService();

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            GetVideos();
        }

        public void GetVideos()
        {
            _videos = videoService.GetVideos();
        }
    }
}
