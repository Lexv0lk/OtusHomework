using Game.GamePlay.Conveyor;
using Game.GamePlay.Conveyor.Components;
using Homework_Upgrades.Upgrades.Configs;
using Zenject;

namespace Homework_Upgrades.Upgrades
{
    public class LoadStorageUpgrade : Upgrade
    {
        private readonly LoadStorageUpgradeConfig _config;
        private ConveyorEntity _conveyorEntity;
        
        public LoadStorageUpgrade(LoadStorageUpgradeConfig config) : base(config)
        {
            _config = config;
        }

        [Inject]
        private void Construct(ConveyorEntity conveyorEntity)
        {
            _conveyorEntity = conveyorEntity;
        }

        protected override void OnUpgrade(int newLevel)
        {
            int loadStorage = _config.StorageUpgradeTable.GetLoadStorage(newLevel);
            _conveyorEntity.Get<IConveyor_SetLoadStorageComponent>().SetLoadStorage(loadStorage);
        }
    }
}