using System;
using System.Collections.Generic;
using Atomic.Objects;
using Game.Scripts.Mechanics.Animation;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyAnimation
    {
        public void Compose(CharacterCore characterCore, CharacterAnimation characterAnimation)
        {
            
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return Array.Empty<IAtomicLogic>();
        }
    }
}