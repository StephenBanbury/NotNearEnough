using System;
using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class ScreenFormation
    {
        public List<ScreenPosition> Cross()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f, 0, 2.8f), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(0.483f, 0, 2.03f), Rotation = 90},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(1.25f, 0, 1.26f), Rotation = 0},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(2.03f, 0, 0.49f), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(2.03f, 0, -1.04f), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(1.25f, 0, -1.8f), Rotation = 180},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(0.483f, 0, -2.57f), Rotation = 90},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f, 0, -3.33f), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f, 0, -3.33f), Rotation = 0},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-2.59f, 0, -2.57f), Rotation = 90},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.35f, 0, -1.8f), Rotation = 0},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f, 0, -1.04f), Rotation = 90},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f, 0, 0.49f), Rotation = 90},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-3.35f, 0, 1.26f), Rotation = 180},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-2.59f, 0, 2.03f), Rotation = 90},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f, 0, 2.8f), Rotation = 180}
            };

            return formation;
        }
    }
}
