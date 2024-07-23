using Popups.CharacterInfoPopup.Presenters;
using Popups.CharacterInfoPopup.Views;
using Popups.Common;
using Zenject;

namespace Popups.CharacterInfoPopup
{
    public sealed class CharacterInfoPopupAdapter : IPopupAdapter
    {
        private readonly CharacterInfoPopupModel _model;
        private readonly CharacterInfoPopupView _view;

        [Inject]
        public CharacterInfoPopupAdapter(CharacterInfoPopupModel model, CharacterInfoPopupView view)
        {
            _model = model;
            _view = view;

            _view.CloseButtonClicked += ClosePopup;
            _view.Initialize(new DefaultCharacterInfoPopupPresenter(_model.UserInfo, _model.CharacterInfo, _model.PlayerLevel));
        }

        ~CharacterInfoPopupAdapter()
        {
            _view.CloseButtonClicked -= ClosePopup;
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