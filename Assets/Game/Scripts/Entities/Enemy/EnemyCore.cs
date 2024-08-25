using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Components;
using Game.Scripts.Mechanics;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyCore
    {
        public AttackComponent AttackComponent;
        
        [SerializeField] private float _followReachDistance = 1;
        [SerializeField] private Collider _collider;
        
        private FollowTargetMechanic _followTargetMechanic;
        private LookAtTargetMechanic _lookAtTargetMechanic;
        private ColliderStateChangeFromDeathMechanic _colliderStateMechanic;
        private AtomicEntity _player;

        public void Construct(AtomicEntity player)
        {
            _player = player;
        }
                
        public void Compose(CharacterCore characterCore, IAtomicValue<Vector3> rootPosition)
        {
            IAtomicValue<Vector3> targetPosition = new AtomicFunction<Vector3>(GetTargetPosition);

            _followTargetMechanic = new FollowTargetMechanic(targetPosition,
                rootPosition, characterCore.MoveComponent.Direction, _followReachDistance);
            
            _lookAtTargetMechanic = new LookAtTargetMechanic(targetPosition,
                rootPosition, characterCore.RotateComponent.ForwardDirection);
            
            _colliderStateMechanic =
                new ColliderStateChangeFromDeathMechanic(characterCore.LifeComponent.IsDead, _collider);
            
            IAtomicAction<int> playerTakeDamageAction = _player.Get<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION);
            AttackComponent.Compose(_followTargetMechanic.IsReachedTarget, playerTakeDamageAction);
        }

        public void Dispose()
        {
            AttackComponent.Dispose();
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            var selfMechanics = new IAtomicLogic[]
                { _followTargetMechanic, _lookAtTargetMechanic, _colliderStateMechanic };
            
            return selfMechanics.Union(AttackComponent.GetMechanics());
        }

        private Vector3 GetTargetPosition()
        {
            return _player.transform.position;
        }
    }
}