using Homework_Upgrades.Upgrades.Tables;
using UnityEngine;

namespace Homework_Upgrades.Upgrades.Configs
{
    [CreateAssetMenu(fileName = "UnloadStorageUpgradeConfig", menuName = "UpgradeConfigs/New UnloadStorageUpgradeConfig")]
    public class UnloadStorageUpgradeConfig : UpgradeConfig
    {
        [SerializeField] private UnloadStorageUpgradeTable _storageUpgradeTable;
        
        public UnloadStorageUpgradeTable StorageUpgradeTable => _storageUpgradeTable;

        protected override void OnValidate()
        {
            base.OnValidate();
            _storageUpgradeTable.Validate(MaxLevel);
        }

        public override Upgrade CreateUpgrade()
        {
            return new UnloadStorageUpgrade(this);
        }
    }
}