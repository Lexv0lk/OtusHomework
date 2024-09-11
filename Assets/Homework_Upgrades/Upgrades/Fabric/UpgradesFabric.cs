using Homework_Upgrades.Upgrades.Configs;
using Zenject;

namespace Homework_Upgrades.Upgrades.Fabric
{
    public class UpgradesFabric
    {
        private readonly DiContainer _container;

        public UpgradesFabric(DiContainer container)
        {
            _container = container;
        }
        
        public Upgrade CreateUpgrade(UpgradeConfig config)
        {
            var upgrade = config.CreateUpgrade();
            _container.Inject(upgrade);
            return upgrade;
        }
    }
}