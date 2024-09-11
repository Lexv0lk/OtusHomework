using Game.GamePlay.Conveyor;
using Homework_Upgrades.Upgrades.Fabric;
using Zenject;

namespace Homework_Upgrades.DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ConveyorEntity>().FromComponentInHierarchy().AsCached();
            Container.Bind<UpgradesFabric>().FromNew().AsSingle().NonLazy();
        }
    }
}