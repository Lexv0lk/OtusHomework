using TMPro;
using UnityEngine;

namespace Session.View
{
    public class SessionLogTimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _enterTime;
        [SerializeField] private TMP_Text _exitTime;
        
        public void SetData(string enterTime, string exitTime)
        {
            _enterTime.text = enterTime;
            _exitTime.text = exitTime;
        }
    }
}