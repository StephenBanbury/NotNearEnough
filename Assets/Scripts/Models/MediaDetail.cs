using System;
using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class MediaDetail
    {
        public int Id;
        public string Title;
        public MediaType MediaType;
        public Source Source;
        public string Url;
        public bool Show;
        public int DisplayId;
    }


    [Serializable]
    public class MediaDetailTest
    {
        public string title;
        public string url;
    }
}
