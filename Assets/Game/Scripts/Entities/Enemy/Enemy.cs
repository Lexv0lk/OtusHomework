using Atomic.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Entities
{
    public class Enemy : Character
    {
        [SerializeField] private EnemyCore _enemyCore;

        [Inject]
        private void Construct(IAtomicEntity player)
        {
            _enemyCore.Construct(player);
        }

        protected override void OnAwake()
        {
            _enemyCore.Compose(CharacterCore, Position);

            foreach (var mechanic in _enemyCore.GetMechanics())
                AddLogic(mechanic);
        }

        protected override void OnUpdate()
        {
            if (CharacterCore.LifeComponent.IsDead.Value)
                return;
            
            OnUpdate(Time.deltaTime);
        }
    }
}