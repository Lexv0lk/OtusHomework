using Game.GamePlay.Conveyor;
using Game.GamePlay.Conveyor.Components;
using Homework_Upgrades.Upgrades.Configs;
using Zenject;

namespace Homework_Upgrades.Upgrades
{
    public class UnloadStorageUpgrade : Upgrade
    {
        private readonly UnloadStorageUpgradeConfig _config;
        private ConveyorEntity _conveyorEntity;
        
        public UnloadStorageUpgrade(UnloadStorageUpgradeConfig config) : base(config)
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
            int unloadStorage = _config.StorageUpgradeTable.GetUnloadStorage(newLevel);
            _conveyorEntity.Get<IConveyor_SetUnloadStorageComponent>().SetUnloadStorage(unloadStorage);
        }
    }
}