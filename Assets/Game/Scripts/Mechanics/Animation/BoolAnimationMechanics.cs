using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Mechanics.Animation
{
    public class BoolAnimationMechanics : IAtomicEnable, IAtomicDisable
    {
        private readonly IAtomicObservable<bool> _condition;
        private readonly Animator _animator;
        private readonly int _animatorKey;

        public BoolAnimationMechanics(IAtomicObservable<bool> condition, Animator animator, int animatorKey)
        {
            _condition = condition;
            _animator = animator;
            _animatorKey = animatorKey;
        }

        public void Enable()
        {
            _condition.Subscribe(OnConditionChanged);
        }

        public void Disable()
        {
            _condition.Unsubscribe(OnConditionChanged);
        }

        private void OnConditionChanged(bool value)
        {
            _animator.SetBool(_animatorKey, value);
        }
    }
}