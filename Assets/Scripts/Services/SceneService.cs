using UnityEngine;

namespace Assets.Scripts.Services
{
    public class SceneService
    {
        public Vector3 Scene1Position()
        {
            return new Vector3(0f, 0f, 0f);
        }
        public Vector3 Scene2Position()
        {
            return new Vector3(20f, 0f, 0f);
        }
        public Vector3 Scene3Position()
        {
            return new Vector3(20f, 0f, 20f);
        }
        public Vector3 Scene4Position()
        {
            return new Vector3(00f, 0f, 20f);
        }
        public Vector3 Scene5Position()
        {
            return new Vector3(-20f, 0f, 0f);
        }
        public Vector3 Scene6Position()
        {
            return new Vector3(-20f, 0f, 20f);
        }
        public Vector3 Scene7Position()
        {
            return new Vector3(0f, 0f, -20f);
        }
        public Vector3 Scene8Position()
        {
            return new Vector3(-20f, 0f, -20f);
        }
    }
}
