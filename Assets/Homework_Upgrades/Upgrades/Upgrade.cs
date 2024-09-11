using Homework_Upgrades.Upgrades.Configs;
using UnityEngine;

namespace Homework_Upgrades.Upgrades
{
    public abstract class Upgrade
    {
        public string Id => _config.Id;
        public int Level => _level;
        public int MaxLevel => _config.MaxLevel;
        public bool IsMaxLevel => _level >= _config.MaxLevel;
        public int NextPrice => _config.PriceTable.GetPrice(_level + 1);
        
        private readonly UpgradeConfig _config;
        private int _level;

        protected Upgrade(UpgradeConfig config)
        {
            _config = config;
            _level = 1;
        }

        public void LevelUp()
        {
            if (IsMaxLevel)
            {
                Debug.LogError("Level is already max");
                return;
            }

            SetupLevel(_level + 1);
        }

        public void SetupLevel(int level)
        {
            _level = level;
            OnUpgrade(level);
        }

        protected abstract void OnUpgrade(int newLevel);
    }
}