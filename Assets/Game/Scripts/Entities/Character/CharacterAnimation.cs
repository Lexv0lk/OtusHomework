using System;
using System.Collections.Generic;
using Atomic.Objects;
using Game.Scripts.Mechanics.Animation;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class CharacterAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;

        [Header("Animation Keys")] 
        [SerializeField] private string _deadBoolean = "IsDead";
        [SerializeField] private string _moveBoolean = "IsMoving";

        [Header("Animation Events")] 
        [SerializeField] private string _deadEvent = "Died";

        private Action _dieAnimationAction;
        
        private BoolAnimationMechanics _moveAnimationMechanics;
        private BoolAnimationMechanics _deadAnimationMechanics;

        public Animator Animator => _animator;
        public AnimatorDispatcher AnimatorDispatcher => _animatorDispatcher;
        
        public void Compose(CharacterCore core, Action dieAnimationAction)
        {
            _dieAnimationAction = dieAnimationAction;
            
            _moveAnimationMechanics = new BoolAnimationMechanics(core.MoveComponent.IsMoving,
                _animator, Animator.StringToHash(_moveBoolean));

            _deadAnimationMechanics = new BoolAnimationMechanics(core.LifeComponent.IsDead, _animator,
                Animator.StringToHash(_deadBoolean));
            
            _animatorDispatcher.SubscribeOnEvent(_deadEvent, _dieAnimationAction);
        }

        public void Dispose()
        {
            _animatorDispatcher.UnsubscribeOnEvent(_deadEvent, _dieAnimationAction);
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new[] { _moveAnimationMechanics, _deadAnimationMechanics };
        }
    }
}