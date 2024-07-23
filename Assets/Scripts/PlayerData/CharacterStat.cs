using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PlayerData
{
    [Serializable]
    public sealed class CharacterStat
    {
        public CharacterStat(string name, int value)
        {
            Name = name;
            Value = value;
        }
        
        public event Action<int> OnValueChanged; 

        [ShowInInspector, ReadOnly]
        public string Name { get; private set; }

        [ShowInInspector, ReadOnly]
        public int Value { get; private set; }

        [Button]
        public void ChangeValue(int value)
        {
            this.Value = value;
            this.OnValueChanged?.Invoke(value);
        }
    }
}