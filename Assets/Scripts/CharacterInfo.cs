using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UniRx;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfo
    {
        [ShowInInspector]
        public readonly ReactiveCollection<CharacterStat> Stats = new();

        [Button]
        public void AddStat(CharacterStat stat)
        {
            Stats.Add(stat);
        }

        [Button]
        public void RemoveStat(CharacterStat stat)
        {
            Stats.Remove(stat);
        }
    }
}