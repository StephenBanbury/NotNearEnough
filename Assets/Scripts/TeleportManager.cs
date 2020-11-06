using UnityEngine;

namespace Assets.Scripts
{
    public class TeleportManager : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                Vector3 newPos = new Vector3(0f, 1f, 0);

                var spawnPoint = GameObject.Find("SpawnPoint1").transform;
                var capsule = GameObject.Find("Capsule").transform;
                var player = GameObject.Find("Player").transform;

                //capsule.position = spawnPoint.position;

                player.position = spawnPoint.position;

                //player.position = player.position + newPos;

                //player.SetParent(spawnPoint);
                //player.position = new Vector3(0f, 0f, 0f);

                


            }
        }

    }

}