using System;
using System.Collections.Generic;
using Homework_Upgrades.Upgrades.Configs;
using Homework_Upgrades.Upgrades.Factory;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homework_Upgrades.Upgrades.Test
{
    public class UpgradesTest : MonoBehaviour
    {
        [SerializeField] private UpgradeConfig[] _configs;

        [Header("Save Test")] 
        [SerializeField] private SaveStub[] _saves;

        private UpgradesController _upgradesController;

        [Inject]
        private void Construct(UpgradesController upgradesController)
        {
            _upgradesController = upgradesController;
        }

        private void Awake()
        {
            foreach (var config in _configs)
                _upgradesController.AddUpgrade(config);
        }

        private void Start()
        {
            foreach (var save in _saves)
                _upgradesController.GetUpgrade(save.UpgradeId).SetupLevel(save.SavedLevel);
        }

        [Button]
        private void UpProduceTime()
        {
            _upgradesController.LevelUp<ProduceTimeUpgrade>();
        }
        
        [Button]
        private void UpLoadStorage()
        {
            _upgradesController.LevelUp<LoadStorageUpgrade>();
        }
        
        [Button]
        private void UpUnloadStorage()
        {
            _upgradesController.LevelUp<UnloadStorageUpgrade>();
        }

        [Serializable]
        private struct SaveStub
        {
            public string UpgradeId;
            public int SavedLevel;
        }
    }
}