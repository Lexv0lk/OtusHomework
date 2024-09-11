using Homework_Upgrades.Upgrades.Tables;
using UnityEngine;

namespace Homework_Upgrades.Upgrades.Configs
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private int _maxLevel;
        [SerializeField] private UpgradePriceTable _priceTable;
        
        public string Id => _id;
        public int MaxLevel => _maxLevel;
        public UpgradePriceTable PriceTable => _priceTable;

        protected virtual void OnValidate()
        {
            _priceTable.Validate(_maxLevel);
        }
        
        public abstract Upgrade CreateUpgrade();
    }
}