using System.Collections;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScreenSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private Text _screenIdText;
        [SerializeField] private int _screenId;
        [SerializeField] private Text _mediaDisplayText;

        private Text _buttonText;

        private void OnTriggerEnter(Collider other)
        {
            _buttonText = gameObject.GetComponentInChildren<Text>();

            if (other.CompareTag("Hand"))
            {

                if (_buttonText.text == "Play")
                {
                    StartCoroutine(Play());
                }
                else
                {
                    StartCoroutine(Pause());
                }
            }
        }

        private IEnumerator Play()
        {
            MediaType currentMediaType = MediaDisplayManager.instance.ScreenSelectAndPlayMedia(_screenId);
            _screenIdText.text = _screenId.ToString();
         
            yield return new WaitForSeconds(0.7f);

            if(currentMediaType == MediaType.VideoClip) _buttonText.text = "Pause";
        }

        private IEnumerator Pause()
        {
            _screenIdText.text = _screenId.ToString();
            MediaDisplayManager.instance.VideoControl(_screenId, VideoControlVariant.Pause);

            yield return new WaitForSeconds(0.7f);

            _buttonText.text = "Play";
        }
    }
}