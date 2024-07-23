using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace PlayerData
{
    [Serializable]
    public sealed class CharacterInfo
    {
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;
    
        [ShowInInspector]
        private readonly HashSet<CharacterStat> stats = new();

        [Button]
        public void AddStat(string name, int value)
        {
            var stat = new CharacterStat(name, value);
            
            if (this.stats.Add(stat))
            {
                this.OnStatAdded?.Invoke(stat);
            }
        }

        [Button]
        public void RemoveStat(string name)
        {
            var stat = this.stats.FirstOrDefault(x => x.Name == name);
            
            if (this.stats.Remove(stat))
            {
                this.OnStatRemoved?.Invoke(stat);
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in this.stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return this.stats.ToArray();
        }
    }
}