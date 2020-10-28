using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }

        public IEnumerator MoveToPosition(Vector3 position, float timeToMove)
        {
            var currentPos = transform.position;
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                transform.position = Vector3.Lerp(currentPos, position, t);
                yield return null;
            }
        }
    }
}