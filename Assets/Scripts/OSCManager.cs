using UnityEngine;

namespace Assets.Scripts
{
    public class OSCManager : MonoBehaviour
    {
        //private OscIn _oscIn;

        //void Start()
        //{
        //    _oscIn = gameObject.AddComponent<OscIn>();
        //    _oscIn.Open(7000);
        //    _oscIn.MapFloat("/test", OnTest);

        //}

        //void OnTest(float value)
        //{
        //    Debug.Log($"OSC received: {value}");
        //}


        public void OnReceiveFloat(float value)
        {
            var test = value * 0.5f + 0.5f;
            Debug.Log(value.ToString());
        }

    }
}
