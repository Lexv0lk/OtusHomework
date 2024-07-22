using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfo
    {
        [ShowInInspector, ReadOnly] 
        public readonly StringReactiveProperty Name;

        [ShowInInspector, ReadOnly] 
        public readonly StringReactiveProperty Description;

        [ShowInInspector, ReadOnly] 
        public readonly ReactiveProperty<Sprite> Icon;

        [Button]
        public void ChangeName(string name)
        {
            Name.Value = name;
        }

        [Button]
        public void ChangeDescription(string description)
        {
            Description.Value = description;
        }

        [Button]
        public void ChangeIcon(Sprite icon)
        {
            Icon.Value = icon;
        }
    }
}