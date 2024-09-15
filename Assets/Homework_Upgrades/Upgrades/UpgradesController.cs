using System.Collections.Generic;
using System.Linq;
using Game.GamePlay.Upgrades;
using Homework_Upgrades.Upgrades.Configs;
using Homework_Upgrades.Upgrades.Factory;
using UnityEngine;

namespace Homework_Upgrades.Upgrades
{
    public class UpgradesController
    {
        private readonly IMoneyStorage _moneyStorage;
        private readonly UpgradesFactory _factory;
        private readonly List<Upgrade> _upgrades = new();

        public UpgradesController(IMoneyStorage moneyStorage, UpgradesFactory factory)
        {
            _moneyStorage = moneyStorage;
            _factory = factory;
        }

        public void LevelUp<T>() where T : Upgrade
        {
            T upgrade = GetUpgrade<T>();

            if (upgrade is null)
            {
                Debug.LogError($"Upgrade of type {typeof(T)} wasn't found");
                return;
            }

            if (upgrade.IsMaxLevel)
            {
                Debug.LogError($"Upgrade is already max leveled");
                return;
            }

            int price = upgrade.NextPrice;

            if (_moneyStorage.CanSpendMoney(price) == false)
            {
                Debug.LogError($"Player doesn't have enough money to level up this upgrade");
                return; 
            }
            
            _moneyStorage.SpendMoney(price);
            upgrade.LevelUp();
        }

        public void AddUpgrade(UpgradeConfig config)
        {
            _upgrades.Add(_factory.CreateUpgrade(config));
        }

        public T GetUpgrade<T>() where T : Upgrade
        {
            foreach (var upgrade in _upgrades)
            {
                if (upgrade is T concreteUpgrade)
                    return concreteUpgrade;
            }

            return default;
        }

        public Upgrade GetUpgrade(string id)
        {
            return _upgrades.FirstOrDefault(up => up.Id == id);
        }
    }
}