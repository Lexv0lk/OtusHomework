using System;
using Sirenix.OdinInspector;
using UniRx;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevel
    {
        [ShowInInspector, ReadOnly] 
        public readonly IntReactiveProperty CurrentLevel = new(1);

        [ShowInInspector, ReadOnly] 
        public readonly IntReactiveProperty CurrentExperience = new();

        [ShowInInspector, ReadOnly]
        public int RequiredExperience
        {
            get { return 100 * (this.CurrentLevel.Value + 1); }
        }

        [Button]
        public void AddExperience(int range)
        {
            var xp = Math.Min(this.CurrentExperience.Value + range, this.RequiredExperience);
            CurrentExperience.Value = xp;
        }

        [Button]
        public void LevelUp()
        {
            if (this.CanLevelUp())
            {
                CurrentExperience.Value = 0;
                CurrentLevel.Value++;
            }
        }

        public bool CanLevelUp()
        {
            return CurrentExperience.Value == RequiredExperience;
        }
    }
}