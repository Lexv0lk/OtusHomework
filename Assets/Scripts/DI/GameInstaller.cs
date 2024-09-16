using Time;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ServerTimeController>().AsSingle().NonLazy();
        }
    }
}