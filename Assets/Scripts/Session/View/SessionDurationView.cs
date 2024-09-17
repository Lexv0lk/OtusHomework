using TMPro;
using UnityEngine;

namespace Session.View
{
    public class SessionDurationView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _durationText;

        public void SetText(string value)
        {
            _durationText.text = value;
        }
    }
}