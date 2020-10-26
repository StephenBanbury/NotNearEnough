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
            {//1.48 0.52
                new ScreenPosition {Id = 1, Vector3 = new Vector3(1.2f + _xPos, 0 + _yPos, 3.32f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(2.73f + _xPos, 0 + _yPos, 3.32f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(3.51f + _xPos, 0 + _yPos, 2.55f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(3.51f + _xPos, 0 + _yPos, 1.01f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(3.51f + _xPos, 0 + _yPos, -0.52f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(3.51f + _xPos, 0 + _yPos, -2.05f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(2.73f + _xPos, 0 + _yPos, -2.81f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.2f + _xPos, 0 + _yPos, -2.81f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.34f + _xPos, 0 + _yPos, -2.81f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.87f + _xPos, 0 + _yPos, -2.81f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-2.63f + _xPos, 0 + _yPos, -2.05f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-2.63f + _xPos, 0 + _yPos, -0.52f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-2.63f + _xPos, 0 + _yPos, 1.01f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-2.63f + _xPos, 0 + _yPos, 2.55f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-1.87f + _xPos, 0 + _yPos, 3.32f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-0.34f + _xPos, 0 + _yPos, 3.32f + _zPos), Rotation = 0}

                // new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 3, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 4, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, 0.49f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 5, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, -1.04f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 6, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, -2.57f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 11, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -2.57f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -1.04f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, 0.49f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> SmallSquare()
        {//2.14 0.71
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(1.86f + _xPos, 0 + _yPos, 2.51f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(3.39f + _xPos, 0 + _yPos, 2.51f + _zPos), Rotation = 0, Hide = true},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.62f + _xPos, 0 + _yPos, 1.74f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(2.62f + _xPos, 0 + _yPos, 0.2f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(2.62f + _xPos, 0 + _yPos, -1.33f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(4.17f + _xPos, 0 + _yPos, -2.86f + _zPos), Rotation = 90, Hide = true},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(3.39f + _xPos, 0 + _yPos, -3.62f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.86f + _xPos, 0 + _yPos, -2.09f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(0.32f + _xPos, 0 + _yPos, -2.09f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.21f + _xPos, 0 + _yPos, -2.09f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-1.97f + _xPos, 0 + _yPos, -2.86f + _zPos), Rotation = 270, Hide = true},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-1.97f + _xPos, 0 + _yPos, -1.33f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-1.97f + _xPos, 0 + _yPos, 0.2f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-1.97f + _xPos, 0 + _yPos, 1.74f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-1.21f + _xPos, 0 + _yPos, 2.51f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(0.32f + _xPos, 0 + _yPos, 2.51f + _zPos), Rotation = 0}

                //new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 0, Hide = true},
                //new ScreenPosition {Id = 3, Vector3 = new Vector3(0.48f + _xPos, 0 + _yPos, 1.03f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 4, Vector3 = new Vector3(0.48f + _xPos, 0 + _yPos, -0.51f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 5, Vector3 = new Vector3(0.48f + _xPos, 0 + _yPos, -2.04f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 6, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, -3.57f + _zPos), Rotation = 90, Hide = true},
                //new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                //new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -2.8f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, -2.8f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -2.8f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 11, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -3.57f + _zPos), Rotation = 270, Hide = true},
                //new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -2.04f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -0.51f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, 1.03f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> Cross()
        {//1.44 0.53
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(1.16f + _xPos, 0 + _yPos, 3.33f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.923f + _xPos, 0 + _yPos, 2.56f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.69f + _xPos, 0 + _yPos, 1.79f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(3.47f + _xPos, 0 + _yPos, 1.02f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(3.47f + _xPos, 0 + _yPos, -0.51f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(2.69f + _xPos, 0 + _yPos, -1.27f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(1.923f + _xPos, 0 + _yPos, -2.04f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.16f + _xPos, 0 + _yPos, -2.8f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.38f + _xPos, 0 + _yPos, -2.8f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.15f + _xPos, 0 + _yPos, -2.04f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-1.91f + _xPos, 0 + _yPos, -1.27f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-2.67f + _xPos, 0 + _yPos, -0.51f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-2.67f + _xPos, 0 + _yPos, 1.02f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-1.91f + _xPos, 0 + _yPos, 1.79f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-1.15f + _xPos, 0 + _yPos, 2.56f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-0.38f + _xPos, 0 + _yPos, 3.33f + _zPos), Rotation = 0}

                //new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 2, Vector3 = new Vector3(0.483f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 3, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, 1.26f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 4, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, 0.49f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 5, Vector3 = new Vector3(2.03f + _xPos, 0 + _yPos, -1.04f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 6, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -1.8f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 7, Vector3 = new Vector3(0.483f + _xPos, 0 + _yPos, -2.57f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, -3.33f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 10, Vector3 = new Vector3(-2.59f + _xPos, 0 + _yPos, -2.57f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -1.8f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, -1.04f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.11f + _xPos, 0 + _yPos, 0.49f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 14, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, 1.26f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 15, Vector3 = new Vector3(-2.59f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> Star()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {//1.38 0.41
                new ScreenPosition {Id = 1, Vector3 = new Vector3(0.76f + _xPos, 0 + _yPos, 2.21f + _zPos), Rotation = 60},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.91f + _xPos, 0 + _yPos, 1.55f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(2.29f + _xPos, 0 + _yPos, 0.88f + _zPos), Rotation = 120},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(2.30f + _xPos, 0 + _yPos, -0.45f + _zPos), Rotation = 60},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(1.91f + _xPos, 0 + _yPos, -1.11f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(0.76f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 120},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(2.63f + _xPos, 0 + _yPos, -3.92f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.10f + _xPos, 0 + _yPos, -3.92f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.44f + _xPos, 0 + _yPos, -3.92f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.97f + _xPos, 0 + _yPos, -3.92f + _zPos), Rotation = 180, Hide = true},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(0.03f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 240},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-1.12f + _xPos, 0 + _yPos, -1.11f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-1.5f + _xPos, 0 + _yPos, -0.45f + _zPos), Rotation = 300},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-1.49f + _xPos, 0 + _yPos, 0.88f + _zPos), Rotation = 240},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-1.12f + _xPos, 0 + _yPos, 1.55f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(0.03f + _xPos, 0 + _yPos, 2.21f + _zPos), Rotation = 300}

                //new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.62f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 60},
                //new ScreenPosition {Id = 2, Vector3 = new Vector3(0.53f + _xPos, 0 + _yPos, 1.14f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 3, Vector3 = new Vector3(0.91f + _xPos, 0 + _yPos, 0.47f + _zPos), Rotation = 120},
                //new ScreenPosition {Id = 4, Vector3 = new Vector3(0.92f + _xPos, 0 + _yPos, -0.86f + _zPos), Rotation = 60},
                //new ScreenPosition {Id = 5, Vector3 = new Vector3(0.53f + _xPos, 0 + _yPos, -1.52f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 6, Vector3 = new Vector3(-0.62f + _xPos, 0 + _yPos, -2.18f + _zPos), Rotation = 120},
                //new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                //new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                //new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.82f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                //new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -4.33f + _zPos), Rotation = 180, Hide = true},
                //new ScreenPosition {Id = 11, Vector3 = new Vector3(-1.35f + _xPos, 0 + _yPos, -2.18f + _zPos), Rotation = 240},
                //new ScreenPosition {Id = 12, Vector3 = new Vector3(-2.5f + _xPos, 0 + _yPos, -1.52f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 13, Vector3 = new Vector3(-2.88f + _xPos, 0 + _yPos, -0.86f + _zPos), Rotation = 300},
                //new ScreenPosition {Id = 14, Vector3 = new Vector3(-2.87f + _xPos, 0 + _yPos, 0.47f + _zPos), Rotation = 240},
                //new ScreenPosition {Id = 15, Vector3 = new Vector3(-2.5f + _xPos, 0 + _yPos, 1.14f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.35f + _xPos, 0 + _yPos, 1.8f + _zPos), Rotation = 300}
            };

            return formation;
        }
        public List<ScreenPosition> Circle()
        {//0.72 1.36
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(0.44f + _xPos, 0 + _yPos, 4.16f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.92f + _xPos, 0 + _yPos, 3.86f + _zPos), Rotation = 22.5f},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(3.17f + _xPos, 0 + _yPos, 3.03f + _zPos), Rotation = 45},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(4.0f + _xPos, 0 + _yPos, 1.77f + _zPos), Rotation = 67.5f},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(4.29f + _xPos, 0 + _yPos, 0.3f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(4.0f + _xPos, 0 + _yPos, -1.18f + _zPos), Rotation = 112.5f},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(3.17f + _xPos, 0 + _yPos, -2.43f + _zPos), Rotation = 135},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.91f + _xPos, 0 + _yPos, -3.27f + _zPos), Rotation = 157.5f},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(0.44f + _xPos, 0 + _yPos, -3.56f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.03f + _xPos, 0 + _yPos, -3.27f + _zPos), Rotation = 202.5f},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-2.29f + _xPos, 0 + _yPos, -2.43f + _zPos), Rotation = 225},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-3.12f + _xPos, 0 + _yPos, -1.18f + _zPos), Rotation = 247.5f},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-3.41f + _xPos, 0 + _yPos, 0.3f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-3.12f + _xPos, 0 + _yPos, 1.77f + _zPos), Rotation = 292.5f},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-2.28f + _xPos, 0 + _yPos, 3.03f + _zPos), Rotation = 315},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.03f + _xPos, 0 + _yPos, 3.86f + _zPos), Rotation = 337.5f}

                //new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 2, Vector3 = new Vector3(1.2f + _xPos, 0 + _yPos, 2.5f + _zPos), Rotation = 22.5f},
                //new ScreenPosition {Id = 3, Vector3 = new Vector3(2.45f + _xPos, 0 + _yPos, 1.67f + _zPos), Rotation = 45},
                //new ScreenPosition {Id = 4, Vector3 = new Vector3(3.28f + _xPos, 0 + _yPos, 0.41f + _zPos), Rotation = 67.5f},
                //new ScreenPosition {Id = 5, Vector3 = new Vector3(3.57f + _xPos, 0 + _yPos, -1.06f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 6, Vector3 = new Vector3(3.28f + _xPos, 0 + _yPos, -2.54f + _zPos), Rotation = 112.5f},
                //new ScreenPosition {Id = 7, Vector3 = new Vector3(2.45f + _xPos, 0 + _yPos, -3.79f + _zPos), Rotation = 135},
                //new ScreenPosition {Id = 8, Vector3 = new Vector3(1.19f + _xPos, 0 + _yPos, -4.63f + _zPos), Rotation = 157.5f},
                //new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -4.92f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.75f + _xPos, 0 + _yPos, -4.63f + _zPos), Rotation = 202.5f},
                //new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.01f + _xPos, 0 + _yPos, -3.79f + _zPos), Rotation = 225},
                //new ScreenPosition {Id = 12, Vector3 = new Vector3(-3.84f + _xPos, 0 + _yPos, -2.54f + _zPos), Rotation = 247.5f},
                //new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.13f + _xPos, 0 + _yPos, -1.06f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 14, Vector3 = new Vector3(-3.84f + _xPos, 0 + _yPos, 0.41f + _zPos), Rotation = 292.5f},
                //new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.0f + _xPos, 0 + _yPos, 1.67f + _zPos), Rotation = 315},
                //new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.75f + _xPos, 0 + _yPos, 2.5f + _zPos), Rotation = 337.5f}
            };

            return formation;
        }
        public List<ScreenPosition> Triangle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {
                new ScreenPosition {Id = 1, Vector3 = new Vector3(0.55f + _xPos, 0 + _yPos, 4.92f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.06f + _xPos, 0 + _yPos, 3.48f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(1.57f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(2.08f + _xPos, 0 + _yPos, 0.58f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(2.59f + _xPos, 0 + _yPos, -0.86f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(3.11f + _xPos, 0 + _yPos, -2.31f + _zPos), Rotation = 70.529f},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(2.59f + _xPos, 0 + _yPos, -3.02f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.06f + _xPos, 0 + _yPos, -3.02f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.47f + _xPos, 0 + _yPos, -3.02f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-2.01f + _xPos, 0 + _yPos, -3.02f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-2.51f + _xPos, 0 + _yPos, -2.31f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-2f + _xPos, 0 + _yPos, -0.86f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-1.49f + _xPos, 0 + _yPos, 0.58f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-0.98f + _xPos, 0 + _yPos, 2.03f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-0.47f + _xPos, 0 + _yPos, 3.48f + _zPos), Rotation = -70.529f},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(0.04f + _xPos, 0 + _yPos, 4.92f + _zPos), Rotation = -70.529f}
                
                //new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = 70.529f},
                //new ScreenPosition {Id = 2, Vector3 = new Vector3(0.23f + _xPos, 0 + _yPos, 1.36f + _zPos), Rotation = 70.529f},
                //new ScreenPosition {Id = 3, Vector3 = new Vector3(0.74f + _xPos, 0 + _yPos, -0.09f + _zPos), Rotation = 70.529f},
                //new ScreenPosition {Id = 4, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -1.54f + _zPos), Rotation = 70.529f},
                //new ScreenPosition {Id = 5, Vector3 = new Vector3(1.76f + _xPos, 0 + _yPos, -2.98f + _zPos), Rotation = 70.529f},
                //new ScreenPosition {Id = 6, Vector3 = new Vector3(2.28f + _xPos, 0 + _yPos, -4.43f + _zPos), Rotation = 70.529f},
                //new ScreenPosition {Id = 7, Vector3 = new Vector3(1.76f + _xPos, 0 + _yPos, -5.14f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 8, Vector3 = new Vector3(0.23f + _xPos, 0 + _yPos, -5.14f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.3f + _xPos, 0 + _yPos, -5.14f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 10, Vector3 = new Vector3(-2.84f + _xPos, 0 + _yPos, -5.14f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.34f + _xPos, 0 + _yPos, -4.43f + _zPos), Rotation = -70.529f},
                //new ScreenPosition {Id = 12, Vector3 = new Vector3(-2.83f + _xPos, 0 + _yPos, -2.98f + _zPos), Rotation = -70.529f},
                //new ScreenPosition {Id = 13, Vector3 = new Vector3(-2.32f + _xPos, 0 + _yPos, -1.54f + _zPos), Rotation = -70.529f},
                //new ScreenPosition {Id = 14, Vector3 = new Vector3(-1.81f + _xPos, 0 + _yPos, -0.09f + _zPos), Rotation = -70.529f},
                //new ScreenPosition {Id = 15, Vector3 = new Vector3(-1.3f + _xPos, 0 + _yPos, 1.36f + _zPos), Rotation = -70.529f},
                //new ScreenPosition {Id = 16, Vector3 = new Vector3(-0.79f + _xPos, 0 + _yPos, 2.8f + _zPos), Rotation = -70.529f}

            };

            return formation;
        }
        public List<ScreenPosition> ShortRectangle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {// 1.48 0.47
                new ScreenPosition {Id = 1, Vector3 = new Vector3(1.2f + _xPos, 0 + _yPos, 1.77f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(2.73f + _xPos, 0 + _yPos, 1.77f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(4.27f + _xPos, 0 + _yPos, 1.77f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(5.03f + _xPos, 0 + _yPos, 1.0f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(5.03f + _xPos, 0 + _yPos, -0.53f), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(4.27f + _xPos, 0 + _yPos, -1.3f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(2.73f + _xPos, 0 + _yPos, -1.3f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.2f + _xPos, 0 + _yPos, -1.3f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.33f + _xPos, 0 + _yPos, -1.3f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.87f + _xPos, 0 + _yPos, -1.3f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.4f + _xPos, 0 + _yPos, -1.3f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.16f + _xPos, 0 + _yPos, -0.53f), Rotation = 270},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.16f + _xPos, 0 + _yPos, 1.0f + _zPos), Rotation = 270},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-3.4f + _xPos, 0 + _yPos, 1.77f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-1.87f + _xPos, 0 + _yPos, 1.77f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-0.33f + _xPos, 0 + _yPos, 1.77f + _zPos), Rotation = 0}

                //    new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 3, Vector3 = new Vector3(2.79f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 4, Vector3 = new Vector3(3.55f + _xPos, 0 + _yPos, 0.53f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 5, Vector3 = new Vector3(3.55f + _xPos, 0 + _yPos, -1), Rotation = 90},
                //new ScreenPosition {Id = 6, Vector3 = new Vector3(2.79f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 7, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 8, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 9, Vector3 = new Vector3(-1.81f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 10, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 11, Vector3 = new Vector3(-4.88f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 12, Vector3 = new Vector3(-5.64f + _xPos, 0 + _yPos, -1), Rotation = 270},
                //new ScreenPosition {Id = 13, Vector3 = new Vector3(-5.64f + _xPos, 0 + _yPos, 0.53f + _zPos), Rotation = 270},
                //new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.88f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.81f + _xPos, 0 + _yPos, 1.3f + _zPos), Rotation = 0}
            };

            return formation;
        }
        public List<ScreenPosition> LongRectangle()
        {
            List<ScreenPosition> formation = new List<ScreenPosition>
            {//0.74 1.21
                new ScreenPosition {Id = 1, Vector3 = new Vector3(0.46f + _xPos, 0 + _yPos, 0.97f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 2, Vector3 = new Vector3(1.99f + _xPos, 0 + _yPos, 0.97f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 3, Vector3 = new Vector3(3.53f + _xPos, 0 + _yPos, 0.97f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 4, Vector3 = new Vector3(5.06f + _xPos, 0 + _yPos, 0.97f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 5, Vector3 = new Vector3(5.79f + _xPos, 0 + _yPos, 0.2f + _zPos), Rotation = 90},
                new ScreenPosition {Id = 6, Vector3 = new Vector3(5.05f + _xPos, 0 + _yPos, -0.56f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 7, Vector3 = new Vector3(3.51f + _xPos, 0 + _yPos, -0.56f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 8, Vector3 = new Vector3(1.98f + _xPos, 0 + _yPos, -0.56f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 9, Vector3 = new Vector3(0.45f + _xPos, 0 + _yPos, -0.56f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.09f + _xPos, 0 + _yPos, -0.56f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 11, Vector3 = new Vector3(-2.62f + _xPos, 0 + _yPos, -0.56f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.15f + _xPos, 0 + _yPos, -0.56f + _zPos), Rotation = 180},
                new ScreenPosition {Id = 13, Vector3 = new Vector3(-4.91f + _xPos, 0 + _yPos, 0.2f + _zPos), Rotation = -90},
                new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.14f + _xPos, 0 + _yPos, 0.97f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 15, Vector3 = new Vector3(-2.61f + _xPos, 0 + _yPos, 0.97f + _zPos), Rotation = 0},
                new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.07f + _xPos, 0 + _yPos, 0.97f + _zPos), Rotation = 0}

                //new ScreenPosition {Id = 1, Vector3 = new Vector3(-0.28f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 2, Vector3 = new Vector3(1.25f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 3, Vector3 = new Vector3(2.79f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 4, Vector3 = new Vector3(4.32f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 5, Vector3 = new Vector3(5.08f + _xPos, 0 + _yPos, -1.01f + _zPos), Rotation = 90},
                //new ScreenPosition {Id = 6, Vector3 = new Vector3(4.31f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 7, Vector3 = new Vector3(2.77f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 8, Vector3 = new Vector3(1.24f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 9, Vector3 = new Vector3(-0.29f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 10, Vector3 = new Vector3(-1.83f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 11, Vector3 = new Vector3(-3.36f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 12, Vector3 = new Vector3(-4.89f + _xPos, 0 + _yPos, -1.77f + _zPos), Rotation = 180},
                //new ScreenPosition {Id = 13, Vector3 = new Vector3(-5.65f + _xPos, 0 + _yPos, -1.01f + _zPos), Rotation = -90},
                //new ScreenPosition {Id = 14, Vector3 = new Vector3(-4.88f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 15, Vector3 = new Vector3(-3.35f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0},
                //new ScreenPosition {Id = 16, Vector3 = new Vector3(-1.81f + _xPos, 0 + _yPos, -0.24f + _zPos), Rotation = 0}
            };

            return formation;
        }
    }
}
