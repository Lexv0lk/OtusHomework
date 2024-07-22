using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.Popups
{
    public class PopupOpenManager : MonoBehaviour
    {
        private CharacterInfoPopupAdapter _characterInfoPopupAdapter;
        
        [Inject]
        private void Construct(CharacterInfoPopupAdapter characterInfoPopupAdapter)
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