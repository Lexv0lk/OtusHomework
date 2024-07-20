using ShootEmUp.PauseSystem.UI;
using ShootEmUp.StartScreen;
using ShootEmUp.StartScreen.UI;
using UnityEngine;
using Zenject;

namespace ShootEmUp.DI.Installers
{
    public class ViewsInstaller : MonoInstaller
    {
        [SerializeField] private PausePlayButton _pausePlayButton;
        [SerializeField] private StartScreenView _startScreenView;
        [SerializeField] private StartTimerView _startTimerView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StartTimerView>().FromInstance(_startTimerView);
            Container.BindInterfacesAndSelfTo<StartScreenView>().FromInstance(_startScreenView);
            Container.BindInterfacesAndSelfTo<PausePlayButton>().FromInstance(_pausePlayButton);
        }
    }
}