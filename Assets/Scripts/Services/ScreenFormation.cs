using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class ScreenFormationService
    {
        private readonly float _xPos;
        private readonly float _yPos;
        private readonly float _zPos;

        public ScreenFormationService(Scene scene)
        {
            var sceneService = new SceneService(scene);
            ScenePosition = sceneService.GetScenePosition();

            _xPos = ScenePosition.x;
            _yPos = ScenePosition.y;
            _zPos = ScenePosition.z;
        }
        
        public Vector3 ScenePosition { set; get; }


        public List<ScreenPosition> LargeSquare()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, 0.49f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, -1.04f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, -2.57f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -2.57f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -1.04f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, 0.49f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> SmallSquare()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 0, Hide = true},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(0.48f + _xPos, 0 + _yPos, 1.03f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(0.48f + _xPos, 0 + _yPos, -0.51f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(0.48f + _xPos, 0 + _yPos, -2.04f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, -3.57f + _zPos), Rotation = 90, Hide = true},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -2.8f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, -2.8f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -2.8f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -3.57f + _zPos), Rotation = 270, Hide = true},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -2.04f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -0.51f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, 1.03f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> Cross()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(0.483f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, 1.26f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, 0.49f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, -1.04f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -1.8f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(0.483f + _xPos, 0 + _yPos, -2.57f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-2.59f + _xPos, 0 + _yPos, -2.57f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -1.8f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -1.04f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, 0.49f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, 1.26f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-2.59f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> Star()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.62f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 60},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(0.53f + _xPos, 0 + _yPos, 1.14f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(0.91f + _xPos, 0 + _yPos, 0.47f + _zPos), Rotation = 120},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(0.92f + _xPos, 0 + _yPos, -0.86f + _zPos), Rotation = 60},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(0.53f + _xPos, 0 + _yPos, -1.52f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(-0.62f + _xPos, 0 + _yPos, -2.18f + _zPos), Rotation = 120},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-1.35f + _xPos, 0 + _yPos, -2.18f + _zPos), Rotation = 240},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-2.5f + _xPos, 0 + _yPos, -1.52f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-2.88f + _xPos, 0 + _yPos, -0.86f + _zPos), Rotation = 300},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-2.87f + _xPos, 0 + _yPos, 0.47f + _zPos), Rotation = 240},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-2.5f + _xPos, 0 + _yPos, 1.14f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.35f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 300}
            };

            return formation;
        }
        public List<ScreenPosition> Circle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.2f + _xPos, 0 + _yPos, 2.5f + _zPos), Rotation = 22.5f},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.45f + _xPos, 0 + _yPos, 1.67f + _zPos), Rotation = 45},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(3.28f + _xPos, 0 + _yPos, 0.41f + _zPos), Rotation = 67.5f},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(3.57f + _xPos, 0 + _yPos, -1.06f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(3.28f + _xPos, 0 + _yPos, -2.54f + _zPos), Rotation = 112.5f},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(2.45f + _xPos, 0 + _yPos, -3.79f + _zPos), Rotation = 135},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.19f + _xPos, 0 + _yPos, -4.63f + _zPos), Rotation = 157.5f},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -4.92f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.75f + _xPos, 0 + _yPos, -4.63f + _zPos), Rotation = 202.5f},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.01f + _xPos, 0 + _yPos, -3.79f + _zPos), Rotation = 225},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-3.84f + _xPos, 0 + _yPos, -2.54f + _zPos), Rotation = 247.5f},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.13f + _xPos, 0 + _yPos, -1.06f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-3.84f + _xPos, 0 + _yPos, 0.41f + _zPos), Rotation = 292.5f},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.0f + _xPos, 0 + _yPos, 1.67f + _zPos), Rotation = 315},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.75f + _xPos, 0 + _yPos, 2.5f + _zPos), Rotation = 337.5f}
            };

            return formation;
        }
        public List<ScreenPosition> Triangle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(0.23f + _xPos, 0 + _yPos, 1.36f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(0.74f + _xPos, 0 + _yPos, -0.09f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -1.54f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(1.76f + _xPos, 0 + _yPos, -2.98f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(2.28f + _xPos, 0 + _yPos, -4.43f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.76f + _xPos, 0 + _yPos, -5.14f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(0.23f + _xPos, 0 + _yPos, -5.14f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.3f + _xPos, 0 + _yPos, -5.14f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-2.84f + _xPos, 0 + _yPos, -5.14f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.34f + _xPos, 0 + _yPos, -4.43f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-2.83f + _xPos, 0 + _yPos, -2.98f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-2.32f + _xPos, 0 + _yPos, -1.54f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-1.81f + _xPos, 0 + _yPos, -0.09f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-1.3f + _xPos, 0 + _yPos, 1.36f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-0.79f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = -70.529f}
            };

            return formation;
        }
        public List<ScreenPosition> ShortRectangle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.79f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(3.55f + _xPos, 0 + _yPos, 0.53f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(3.55f + _xPos, 0 + _yPos, -1), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(2.79f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.81f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-4.88f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-5.64f + _xPos, 0 + _yPos, -1), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-5.64f + _xPos, 0 + _yPos, 0.53f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.88f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.81f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> LongRectangle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.79f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(4.32f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(5.08f + _xPos, 0 + _yPos, -1.01f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(4.31f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(2.77f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.24f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.29f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.83f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.36f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.89f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-5.65f + _xPos, 0 + _yPos, -1.01f + _zPos), Rotation = -90},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.88f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.81f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0}
            };

            return formation;
        }
    }
}
