using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Entities
{
    public class Enemy : Character
    {
        [SerializeField] private EnemyCore _enemyCore;

        [SerializeField] private EnemyAnimation _enemyAnimation;

        private IAtomicValueObservable<bool> _isPlayerDead;

        [Inject]
        private void Construct(AtomicEntity player)
        {
            _enemyCore.Construct(player);
            _isPlayerDead = player.Get<IAtomicValueObservable<bool>>(LifeAPI.IS_DEAD);
        }

        protected override void OnAwake()
        {
            _enemyCore.Compose(CharacterCore, new AtomicFunction<Vector3>(GetPosition));
            _enemyAnimation.Compose(_enemyCore, CharacterAnimation, _isPlayerDead);
            
            CharacterCore.MoveComponent.AppendCondition(IsNotInAttack);
            CharacterCore.MoveComponent.AppendCondition(IsPlayerAlive);
            CharacterCore.RotateComponent.AppendCondition(IsPlayerAlive);
            
            _isPlayerDead.Subscribe(OnPlayerDeadStateChanged);

            foreach (var mechanic in _enemyCore.GetMechanics())
                AddLogic(mechanic);
            
            foreach (var mechanic in _enemyAnimation.GetMechanics())
                AddLogic(mechanic);
        }

        protected override void OnUpdate()
        {
            if (CharacterCore.LifeComponent.IsDead.Value)
                return;
            
            OnUpdate(Time.deltaTime);
        }

        protected override void OnReset()
        {
            _enemyCore.AttackComponent.Reset();
        }

        protected override void OnDispose()
        {
            _enemyCore.Dispose();
            _enemyAnimation.Dispose();
            _isPlayerDead.Unsubscribe(OnPlayerDeadStateChanged);
        }

        private Vector3 GetPosition()
        {
            return transform.position;
        }

        private void OnPlayerDeadStateChanged(bool newState)
        {
            if (newState)
            {
                CharacterCore.MoveComponent.Direction.Value = Vector3.zero;
            }
        }

        private bool IsNotInAttack()
        {
            return !_enemyCore.AttackComponent.IsInAttack.Value;
        }

        private bool IsPlayerAlive()
        {
            return !_isPlayerDead.Value;
        }
    }
}