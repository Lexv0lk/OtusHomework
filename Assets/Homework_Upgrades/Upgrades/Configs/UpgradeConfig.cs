using System;
using Homework_Upgrades.Upgrades.Tables;
using UnityEngine;

namespace Homework_Upgrades.Upgrades.Configs
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private int _maxLevel;
        [SerializeField] private UpgradePriceTable _upgradePriceTable;

        protected virtual void OnValidate()
        {
            _upgradePriceTable.Validate(_maxLevel);
        }
    }

    public class LoadStorageUpgradeConfig : UpgradeConfig
    {
        
    }
}