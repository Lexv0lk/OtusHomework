using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics.Animation;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerAnimation
    {
        [SerializeField] private float _animationValueChangeSpeed = 10;
        
        [Header("Animation Keys")] 
        [SerializeField] private string _moveXFloat = "MoveX";
        [SerializeField] private string _moveYFloat = "MoveZ";
        
        private Vector2AnimationMechanics _vector2AnimationMechanics;
        private InverseMoveDirectionMechanics _inverseMoveDirectionMechanics;

        public void Compose(CharacterCore characterCore, CharacterAnimation characterAnimation, Transform transform)
        {
            _inverseMoveDirectionMechanics =
                new InverseMoveDirectionMechanics(transform, characterCore.MoveComponent.Direction);
            
            _vector2AnimationMechanics = new Vector2AnimationMechanics(_inverseMoveDirectionMechanics.ConvertedDirection,
                characterAnimation.Animator, Animator.StringToHash(_moveXFloat), Animator.StringToHash(_moveYFloat), _animationValueChangeSpeed);
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new IAtomicLogic[] { _vector2AnimationMechanics, _inverseMoveDirectionMechanics };
        }
    }
}