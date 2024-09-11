using System;
using System.Collections.Generic;
using Homework_Upgrades.Upgrades.Configs;
using Homework_Upgrades.Upgrades.Fabric;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homework_Upgrades.Upgrades.Test
{
    public class UpgradesTest : MonoBehaviour
    {
        private readonly List<Upgrade> _upgrades = new();
        
        [SerializeField] private ProduceTimeUpgradeConfig _produceTimeUpgradeConfig;
        [SerializeField] private LoadStorageUpgradeConfig _loadStorageUpgradeConfig;
        [SerializeField] private UnloadStorageUpgradeConfig _unloadStorageUpgradeConfig;

        [Header("Save Test")] 
        [SerializeField] private SaveStub[] _saves;

        private UpgradesFabric _upgradesFabric;
        
        private Upgrade _produceTimeUpgrade;
        private Upgrade _loadStorageUpgrade;
        private Upgrade _unloadStorageUpgrade;

        [Inject]
        private void Construct(UpgradesFabric fabric)
        {
            _upgradesFabric = fabric;
        }

        private void Awake()
        {
            _produceTimeUpgrade = _upgradesFabric.CreateUpgrade(_produceTimeUpgradeConfig);
            _loadStorageUpgrade = _upgradesFabric.CreateUpgrade(_loadStorageUpgradeConfig);
            _unloadStorageUpgrade = _upgradesFabric.CreateUpgrade(_unloadStorageUpgradeConfig);
            
            _upgrades.Add(_produceTimeUpgrade);
            _upgrades.Add(_loadStorageUpgrade);
            _upgrades.Add(_unloadStorageUpgrade);
        }

        private void Start()
        {
            foreach (var save in _saves)
                foreach (var upgrade in _upgrades)
                    if (save.UpgradeId == upgrade.Id)
                        upgrade.SetupLevel(save.SavedLevel);
        }

        [Button]
        private void UpProduceTime()
        {
            _produceTimeUpgrade.LevelUp();
        }
        
        [Button]
        private void UpLoadStorage()
        {
            _loadStorageUpgrade.LevelUp();
        }
        
        [Button]
        private void UpUnloadStorage()
        {
            _unloadStorageUpgrade.LevelUp();
        }

        [Serializable]
        private struct SaveStub
        {
            public string UpgradeId;
            public int SavedLevel;
        }
    }
}