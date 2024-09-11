using Homework_Upgrades.Upgrades.Tables;
using UnityEngine;

namespace Homework_Upgrades.Upgrades.Configs
{
    [CreateAssetMenu(fileName = "LoadStorageUpgradeConfig", menuName = "UpgradeConfigs/New LoadStorageUpgradeConfig")]
    public class LoadStorageUpgradeConfig : UpgradeConfig
    {
        [SerializeField] private LoadStorageUpgradeTable _storageUpgradeTable;
        
        public LoadStorageUpgradeTable StorageUpgradeTable => _storageUpgradeTable;

        protected override void OnValidate()
        {
            base.OnValidate();
            _storageUpgradeTable.Validate(MaxLevel);
        }

        public override Upgrade CreateUpgrade()
        {
            return new LoadStorageUpgrade(this);
        }
    }
}