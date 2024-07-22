using UnityEngine;

namespace Lessons.Architecture.PM.Popups
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