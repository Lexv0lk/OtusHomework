using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterStat
    {
        [ShowInInspector, ReadOnly] 
        public readonly StringReactiveProperty Name = new();

        [ShowInInspector, ReadOnly] 
        public readonly IntReactiveProperty Level = new();

        [Button]
        public void ChangeLevel(int value)
        {
            Level.Value = value;
        }
        
        [Button]
        public void ChangeName(string value)
        {
            Name.Value = value;
        }
    }
}