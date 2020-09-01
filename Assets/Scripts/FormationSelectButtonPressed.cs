using UnityEngine;

namespace Assets.Scripts
{
    public class FormationSelectButtonPressed : MonoBehaviour
    {
        [SerializeField] private int _formationId;

        private FormationSelect _formationSelectDisplay;

        void Start()
        {
            _formationSelectDisplay = gameObject.GetComponentInParent<FormationSelect>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
            {
                _formationSelectDisplay.SetFormationId(_formationId);
                _formationSelectDisplay.KeepInSync();
            }
        }

    }
}