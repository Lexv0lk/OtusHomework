using Homework_Upgrades.Upgrades.Tables;
using UnityEngine;

namespace Homework_Upgrades.Upgrades.Configs
{
    [CreateAssetMenu(fileName = "ProduceTimeUpgradeConfig", menuName = "UpgradeConfigs/New ProduceTimeUpgradeConfig")]
    public class ProduceTimeUpgradeConfig : UpgradeConfig
    {
        [SerializeField] private ProduceTimeUpgradeTable _upgradeTable;
        
        public ProduceTimeUpgradeTable UpgradeTable => _upgradeTable;

        protected override void OnValidate()
        {
            base.OnValidate();
            _upgradeTable.Validate(MaxLevel);
        }

        public override Upgrade CreateUpgrade()
        {
            return new ProduceTimeUpgrade(this);
        }
    }
}