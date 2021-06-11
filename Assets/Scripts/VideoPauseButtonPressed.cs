using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class VideoPauseButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _screenId;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                MediaDisplayManager.instance.VideoControl(_screenId, VideoControlVariant.Pause);
            }
        }
    }
}