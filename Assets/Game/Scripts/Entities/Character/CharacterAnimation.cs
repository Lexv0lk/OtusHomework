using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics.Animation;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class CharacterAnimation
    {
        [SerializeField] private Animator _animator;

        [Header("Animation Keys")] 
        [SerializeField] private string _deadBoolean = "IsDead";
        [SerializeField] private string _moveBoolean = "IsMoving";

        private BoolAnimationMechanics _moveAnimationMechanics;

        public Animator Animator => _animator;
        
        public void Compose(CharacterCore core)
        {
            _moveAnimationMechanics = new BoolAnimationMechanics(core.MoveComponent.IsMoving,
                _animator, Animator.StringToHash(_moveBoolean));
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new[] { _moveAnimationMechanics };
        }
    }
}