using Game.GamePlay.Conveyor;
using Game.GamePlay.Upgrades;
using Homework_Upgrades.Upgrades;
using Homework_Upgrades.Upgrades.Factory;
using Zenject;

namespace Homework_Upgrades.DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ConveyorEntity>().FromComponentInHierarchy().AsCached();
            Container.Bind<IMoneyStorage>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<UpgradesFactory>().FromNew().AsSingle();
            Container.Bind<UpgradesController>().FromNew().AsSingle().NonLazy();
        }
    }
}