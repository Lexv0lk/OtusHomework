using PlayerData;
using UnityEngine;
using CharacterInfo = PlayerData.CharacterInfo;

namespace Popups.CharacterInfoPopup
{
    public sealed class CharacterInfoPopupModel : MonoBehaviour
    {
        [SerializeField] private CharacterInfo _characterInfo;
        [SerializeField] private UserInfo _userInfo;
        [SerializeField] private PlayerLevel _playerLevel;

        public CharacterInfo CharacterInfo => _characterInfo;
        public UserInfo UserInfo => _userInfo;
        public PlayerLevel PlayerLevel => _playerLevel;
    }
}