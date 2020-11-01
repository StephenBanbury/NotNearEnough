using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Proyecto26;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class ApiService
    {
        public List<MediaDetail> VideosGet()
        {
            try
            {
                var uri = "https://not-near-enough.firebaseio.com/";
                var videoList = new List<MediaDetail>();

                RestClient.GetArray<MediaDetail>(uri + ".json").Then(response =>
                {
                    if (response != null)
                    {
                        Debug.Log($"VideosGet: {response}");
                        var i = 1;
                        foreach (var video in response.Where(v => v.Title != null))
                        {
                            Debug.Log($"VideosGet: {video.Title} / {video.Url}");
                            video.MediaType = MediaType.VideoClip;
                            video.Source = Source.Url;
                            video.Id = i;
                            videoList.Add(video);
                            i++;
                        }
                    }

                    return videoList;
                });

                return videoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}