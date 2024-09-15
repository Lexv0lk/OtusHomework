using Homework_Upgrades.Upgrades.Configs;
using Zenject;

namespace Homework_Upgrades.Upgrades.Factory
{
    public class UpgradesFactory
    {
        private readonly DiContainer _container;

        public UpgradesFactory(DiContainer container)
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