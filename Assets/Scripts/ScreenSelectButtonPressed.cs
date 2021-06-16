using System.Collections;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScreenSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _screenId;

        private Renderer _rend;
        private Color _colourPlay;
        private Color _colourPause;
        private Text _buttonText;
        private bool _isPlaying;

        private void Start()
        {
            _rend = gameObject.GetComponent<Renderer>();

            _colourPlay = GetComponent<Renderer>().material.GetColor("_Color");
            _colourPause = Color.red;

            _buttonText = gameObject.GetComponentInChildren<Text>();
            _buttonText.text = "Play";

            _isPlaying = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                if (_isPlaying)
                {
                    StartCoroutine(Pause());
                }
                else
                {
                    StartCoroutine(Play());
                }
            }
        }

        private IEnumerator Play()
        {
            MediaType currentMediaType = MediaDisplayManager.instance.ScreenSelectAndPlayMedia(_screenId);

            _buttonText.text = "Pause";

            _rend.material.SetColor("_Color", _colourPause);

            // TODO: Add sound or haptics?

            yield return new WaitForSeconds(0.7f);

            _isPlaying = true;
        }

        private IEnumerator Pause()
        {
            MediaDisplayManager.instance.VideoControl(_screenId, VideoControlVariant.Pause);

            _buttonText.text = "Play";

            _rend.material.SetColor("_Color", _colourPlay);

            // TODO: Add sound or haptics?

            yield return new WaitForSeconds(0.7f);

            _isPlaying = false;
        }
    }
}