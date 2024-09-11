using Game.GamePlay.Conveyor;
using Game.GamePlay.Conveyor.Components;
using Homework_Upgrades.Upgrades.Configs;
using Zenject;

namespace Homework_Upgrades.Upgrades
{
    public class ProduceTimeUpgrade : Upgrade
    {
        private readonly ProduceTimeUpgradeConfig _config;
        private ConveyorEntity _conveyorEntity;
        
        public ProduceTimeUpgrade(ProduceTimeUpgradeConfig config) : base(config)
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
            float produceTime = _config.UpgradeTable.GetProduceTime(newLevel);
            _conveyorEntity.Get<IConveyor_SetProduceTimeComponent>().SetProduceTime(produceTime);
        }
    }
}