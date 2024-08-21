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
        private IAtomicEntity _player;

        public void Construct(IAtomicEntity player)
        {
            _player = player;
        }
                
        public void Compose(CharacterCore characterCore, IAtomicValue<Vector3> rootPosition)
        {
            _followTargetMechanic = new FollowTargetMechanic(_player.Get<IAtomicValue<Vector3>>(TransformAPI.POSITION),
                rootPosition, characterCore.MoveComponent.Direction, _followReachDistance);
            
            _lookAtTargetMechanic = new LookAtTargetMechanic(_player.Get<IAtomicValue<Vector3>>(TransformAPI.POSITION),
                rootPosition, characterCore.RotateComponent.ForwardDirection);

            IAtomicAction<int> playerTakeDamageAction = _player.Get<IAtomicAction<int>>(LifeAPI.TAKE_DAMAGE);
            
            _reloadingHitMechanic = new ReloadingHitMechanic(_followTargetMechanic.IsReachedTarget,
                playerTakeDamageAction, _hitReloadTime, _damage);
        }

        public IEnumerable<IAtomicLogic> GetMechanics()
        {
            return new IAtomicLogic[] { _followTargetMechanic, _lookAtTargetMechanic, _reloadingHitMechanic };
        }
    }
}