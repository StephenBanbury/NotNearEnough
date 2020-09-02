using System;
using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class ScreenFormation
    {
        public List<ScreenPosition> LargeSquare()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f, 0, 2.8f), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f, 0, 2.8f), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.03f, 0, 2.03f), Rotation = 90},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(2.03f, 0, 0.49f), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(2.03f, 0, -1.04f), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(2.03f, 0, -2.57f), Rotation = 90},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f, 0, -3.33f), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f, 0, -3.33f), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f, 0, -3.33f), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f, 0, -3.33f), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-4.11f, 0, -2.57f), Rotation = 270},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f, 0, -1.04f), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f, 0, 0.49f), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.11f, 0, 2.03f), Rotation = 270},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f, 0, 2.8f), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f, 0, 2.8f), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> SmallSquare()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f, 0, 1.8f), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f, 0, 1.8f), Rotation = 0, Hide = true},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(0.48f, 0, 1.03f), Rotation = 90},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(0.48f, 0, -0.51f), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(0.48f, 0, -2.04f), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(2.03f, 0, -3.57f), Rotation = 90, Hide = true},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f, 0, -4.33f), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f, 0, -2.8f), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f, 0, -2.8f), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f, 0, -2.8f), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-4.11f, 0, -3.57f), Rotation = 270, Hide = true},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f, 0, -2.04f), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f, 0, -0.51f), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.11f, 0, 1.03f), Rotation = 270},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f, 0, 1.8f), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f, 0, 1.8f), Rotation = 0}
            };

            return formation;
        }
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
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f, 0, -3.33f), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-2.59f, 0, -2.57f), Rotation = 270},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.35f, 0, -1.8f), Rotation = 180},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f, 0, -1.04f), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f, 0, 0.49f), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-3.35f, 0, 1.26f), Rotation = 0},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-2.59f, 0, 2.03f), Rotation = 270},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f, 0, 2.8f), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> Star()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.62f, 0, 1.8f), Rotation = 60},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(0.53f, 0, 1.14f), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(0.91f, 0, 0.47f), Rotation = 120},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(0.92f, 0, -0.86f), Rotation = 60},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(0.53f, 0, -1.52f), Rotation = 180},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(-0.62f, 0, -2.18f), Rotation = 120},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f, 0, -4.33f), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f, 0, -4.33f), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f, 0, -4.33f), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f, 0, -4.33f), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-1.35f, 0, -2.18f), Rotation = 240},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-2.5f, 0, -1.52f), Rotation = 180},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-2.88f, 0, -0.86f), Rotation = 300},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-2.87f, 0, 0.47f), Rotation = 240},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-2.5f, 0, 1.14f), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.35f, 0, 1.8f), Rotation = 300}
            };

            return formation;
        }
        public List<ScreenPosition> Circle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f, 0, 2.8f), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.2f, 0, 2.5f), Rotation = 22.5f},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.45f, 0, 1.67f), Rotation = 45},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(3.28f, 0, 0.41f), Rotation = 67.5f},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(3.57f, 0, -1.06f), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(3.28f, 0, -2.54f), Rotation = 112.5f},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(2.45f, 0, -3.79f), Rotation = 135},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.19f, 0, -4.63f), Rotation = 157.5f},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.28f, 0, -4.92f), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.75f, 0, -4.63f), Rotation = 202.5f},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.01f, 0, -3.79f), Rotation = 225},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-3.84f, 0, -2.54f), Rotation = 247.5f},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.13f, 0, -1.06f), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-3.84f, 0, 0.41f), Rotation = 292.5f},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.0f, 0, 1.67f), Rotation = 315},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.75f, 0, 2.5f), Rotation = 337.5f}
            };

            return formation;
        }
        public List<ScreenPosition> Triangle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f, 0, 2.8f), Rotation = 70.529f},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(0.23f, 0, 1.36f), Rotation = 70.529f},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(0.74f, 0, -0.09f), Rotation = 70.529f},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(1.25f, 0, -1.54f), Rotation = 70.529f},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(1.76f, 0, -2.98f), Rotation = 70.529f},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(2.28f, 0, -4.43f), Rotation = 70.529f},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.76f, 0, -5.14f), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(0.23f, 0, -5.14f), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.3f, 0, -5.14f), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-2.84f, 0, -5.14f), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.34f, 0, -4.43f), Rotation = -70.529f},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-2.83f, 0, -2.98f), Rotation = -70.529f},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-2.32f, 0, -1.54f), Rotation = -70.529f},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-1.81f, 0, -0.09f), Rotation = -70.529f},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-1.3f, 0, 1.36f), Rotation = -70.529f},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-0.79f, 0, 2.8f), Rotation = -70.529f}
            };

            return formation;
        }
        public List<ScreenPosition> ShortRectangle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f, 0, 1.3f), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f, 0, 1.3f), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.79f, 0, 1.3f), Rotation = 0},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(3.55f, 0, 0.53f), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(3.55f, 0, -1), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(2.79f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.81f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-4.88f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-5.64f, 0, -1), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-5.64f, 0, 0.53f), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.88f, 0, 1.3f), Rotation = 0},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f, 0, 1.3f), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.81f, 0, 1.3f), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> LongRectangle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f, 0, -0.24f), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f, 0, -0.24f), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.79f, 0, -0.24f), Rotation = 0},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(4.32f, 0, -0.24f), Rotation = 0},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(5.08f, 0, -1.01f), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(4.31f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(2.77f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.24f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.29f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.83f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.36f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.89f, 0, -1.77f), Rotation = 180},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-5.65f, 0, -1.01f), Rotation = -90},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.88f, 0, -0.24f), Rotation = 0},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f, 0, -0.24f), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.81f, 0, -0.24f), Rotation = 0}
            };

            return formation;
        }
    }
}
