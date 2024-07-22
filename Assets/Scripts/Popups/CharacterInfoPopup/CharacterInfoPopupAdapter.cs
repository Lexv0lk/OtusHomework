using Popups.CharacterInfoPopup;
using Popups.CharacterInfoPopup.Presenters;
using Zenject;

namespace Lessons.Architecture.PM.Popups
{
    public sealed class CharacterInfoPopupAdapter
    {
        private readonly CharacterInfoPopupModel _model;
        private readonly CharacterInfoPopupView _view;

        [Inject]
        public CharacterInfoPopupAdapter(CharacterInfoPopupModel model, CharacterInfoPopupView view)
        {
            _model = model;
            _view = view;
            
            _view.Initialize(new DefaultCharacterInfoPopupPresenter(_model.UserInfo, _model.CharacterInfo, _model.PlayerLevel));
        }

        public void OpenPopup()
        {
            _view.gameObject.SetActive(true);
        }

        public void ClosePopup()
        {
            _view.gameObject.SetActive(false);
        }
    }
}