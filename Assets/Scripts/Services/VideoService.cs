using System.Collections.Generic;
using Assets.Scripts.Models;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class VideoService
    {
        public List<MediaDetail> GetVideos()
        {
            var returnVideos = new List<MediaDetail>();
            returnVideos.AddRange(testVideos());

            return returnVideos;
        }

        private List<MediaDetail> testVideos()
        {
            var returnVideos = new List<MediaDetail>
            {
                new MediaDetail
                {
                    Id = 1,
                    Title = "Video1",
                    MediaType = MediaType.VideoClip,
                    Source = Source.VideoClip
                },
                new MediaDetail
                {
                    Id = 2,
                    Title = "Video2",
                    MediaType = MediaType.VideoClip,
                    Source = Source.VideoClip
                },
                new MediaDetail
                {
                    Id = 3,
                    Title = "Video3",
                    MediaType = MediaType.VideoClip,
                    Source = Source.VideoClip
                },
                new MediaDetail
                {
                    Id = 4,
                    Title = "Video4",
                    MediaType = MediaType.VideoClip,
                    Source = Source.VideoClip
                },
                new MediaDetail
                {
                    Id = 5,
                    Title = "Video5",
                    MediaType = MediaType.VideoClip,
                    Source = Source.VideoClip
                },
                new MediaDetail
                {
                    Id = 6,
                    Title = "Video6",
                    MediaType = MediaType.VideoClip,
                    Source = Source.Url,
                    Url = "https://giant.gfycat.com/KeyKindheartedCleanerwrasse.webm"
                }
            };

            return returnVideos;
        }

    }
}