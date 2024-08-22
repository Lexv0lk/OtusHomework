using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Mechanics;
using Game.Scripts.Tech;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class EnemyCore
    {
        [SerializeField] private float _followReachDistance = 1;
        [SerializeField] private int _damage;
        [SerializeField] private float _hitReloadTime;
        
        private FollowTargetMechanic _followTargetMechanic;
        private LookAtTargetMechanic _lookAtTargetMechanic;
        private ReloadingHitMechanic _reloadingHitMechanic;
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

            IAtomicAction<int> playerTakeDamageAction = _player.Get<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE_ACTION);
            
            _reloadingHitMechanic = new ReloadingHitMechanic(_followTargetMechanic.IsReachedTarget,
                playerTakeDamageAction, _hitReloadTime, _damage);
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new IAtomicLogic[] { _followTargetMechanic, _lookAtTargetMechanic, _reloadingHitMechanic };
        }

        private Vector3 GetTargetPosition()
        {
            return _player.transform.position;
        }
    }
}