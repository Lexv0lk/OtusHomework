using Popups.CharacterInfoPopup;

namespace Lessons.Architecture.PM.Popups
{
    public sealed class CharacterInfoPopupModel
    {
        public CharacterInfoPopupModel(CharacterInfo characterInfo, UserInfo userInfo, PlayerLevel playerLevel)
        {
            CharacterInfo = characterInfo;
            UserInfo = userInfo;
            PlayerLevel = playerLevel;
        }

        public CharacterInfo CharacterInfo { get; }
        public UserInfo UserInfo { get; }
        public PlayerLevel PlayerLevel { get; }
    }
    
    public sealed class CharacterInfoPopupController
    {
        public CharacterInfoPopupController(CharacterInfoPopupModel model, CharacterInfoPopupView view)
        {
            
        }
    }
}