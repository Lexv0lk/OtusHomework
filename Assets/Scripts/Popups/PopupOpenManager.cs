using Popups.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Popups
{
    public class PopupOpenManager : MonoBehaviour
    {
        private IPopupAdapter _characterInfoPopupAdapter;
        
        [Inject]
        private void Construct(IPopupAdapter characterInfoPopupAdapter)
        {
            _characterInfoPopupAdapter = characterInfoPopupAdapter;
        }

        [Button]
        private void OpenCharacterInfo()
        {
            _characterInfoPopupAdapter.OpenPopup();
        }

        [Button] 
        private void CloseCharacterInfo()
        {
            _characterInfoPopupAdapter.ClosePopup();
        }
    }
}