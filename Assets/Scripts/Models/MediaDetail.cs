using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class MediaDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public MediaType MediaType { get; set; }
        public Source Source { get; set; }
        public string Url { get; set; }
        public bool Show { get; set; }
    }
}
