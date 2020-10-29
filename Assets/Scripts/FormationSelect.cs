using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FormationSelect : MonoBehaviour
    {
        //[SerializeField] private Text _formationIdText;

        private int _formationId;
        private int _previousId;
        
        private FormationSelectSync _formationSelectSync;


        void Start()
        {
            //_formationSelectSync = gameObject.GetComponentInParent<FormationSelectSync>();
            _formationSelectSync = gameObject.GetComponent<FormationSelectSync>();
        }

        public void SetFormationId(int id, int animationSeconds)
        {
            _formationId = id;

            if (_formationId > 0 && _formationId != _previousId)
            {
                //_formationIdText.text = _formationId.ToString();

                MediaDisplayManager.instance.TweenScreens((ScreenFormation)_formationId, animationSeconds);
            }
        }

        public void SetFormationId(Scene scene, int id, int animationSeconds)
        {
            _formationId = id;

            if (_formationId > 0 && _formationId != _previousId)
            {
                //_formationIdText.text = _formationId.ToString();

                MediaDisplayManager.instance.TweenScreens(scene, (ScreenFormation)_formationId, animationSeconds);
            }
        }

        public void KeepInSync()
        {
            // If the id has changed, call SetId on the sync component
            if (_formationId != _previousId)
            {
                _formationSelectSync.SetId(_formationId);
                _previousId = _formationId;
            }
        }
    }
}