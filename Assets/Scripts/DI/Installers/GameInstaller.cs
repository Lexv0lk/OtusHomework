using Popups;
using Popups.CharacterInfoPopup;
using Popups.CharacterInfoPopup.Views;
using Popups.Common;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CharacterInfoPopupModel _characterInfoPopupModel;
        [SerializeField] private PopupOpenManager _popupOpenManager;
        [SerializeField] private CharacterInfoPopupView _characterInfoPopupView;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterInfoPopupView>().FromInstance(_characterInfoPopupView);
            Container.Bind<CharacterInfoPopupModel>().FromInstance(_characterInfoPopupModel);
            Container.Bind<IPopupAdapter>().To<CharacterInfoPopupAdapter>().AsSingle();
            Container.Bind<PopupOpenManager>().FromInstance(_popupOpenManager);
        }
    }
}