using System;
using System.Collections.Generic;
using Chests.Configs;
using Rewards;
using UnityEngine;

namespace Chests
{
    public class Chest
    {
        public string Name { get; }
        public Sprite Icon { get; }
        public float CloseDuration { get; }
        public IEnumerable<IReward> Rewards { get; }
        public DateTime CreateTime { get; private set; }
        
        public ChestConfig Config { get; }
        
        public Chest(ChestConfig config, DateTime createTime)
        {
            Config = config;
            Name = config.Name;
            Icon = config.Icon;
            CloseDuration = config.CloseDuration;
            Rewards = config.Rewards;
            CreateTime = createTime;
        }
        
        public Chest(string name, Sprite icon, float closeDuration,
            IEnumerable<IReward> rewards, DateTime createTime)
        {
            Name = name;
            Icon = icon;
            CloseDuration = closeDuration;
            Rewards = rewards;
            CreateTime = createTime;
        }
    }
}