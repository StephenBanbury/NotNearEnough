using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class SceneDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ScreenFormation ScreenFormation { get; set; }
        public Vector3 ScenePosition { get; set; }
        public bool IsCurrent { get; set; }
    }
}
