using Lessons.Architecture.PM.Popups;
using Popups.CharacterInfoPopup;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.DI.Installers
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
            Container.Bind<CharacterInfoPopupAdapter>().AsSingle().NonLazy();
            Container.Bind<PopupOpenManager>().FromInstance(_popupOpenManager);
        }
    }
}