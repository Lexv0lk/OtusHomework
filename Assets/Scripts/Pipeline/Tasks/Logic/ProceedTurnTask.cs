using Entities;
using Entities.Components;
using EventBus.Events;
using Models;
using UnityEngine;
using Utils;

namespace Pipeline.Tasks.Logic
{
    public class ProceedTurnTask : EventTask
    {
        private readonly TeamsSetup _teamsSetup;
        private readonly EventBus.EventBus _eventBus;
        private readonly CurrentTurn _currentTurn;

        public ProceedTurnTask(TeamsSetup teamsSetup, EventBus.EventBus eventBus, CurrentTurn currentTurn)
        {
            _teamsSetup = teamsSetup;
            _eventBus = eventBus;
            _currentTurn = currentTurn;
        }
        
        protected override void OnRun()
        {
            var attackerEntity = _currentTurn.EntityInTurn;
            var targetEntity = _currentTurn.TargetEntity;
            
            if (attackerEntity.TryGet<RandomTargetComponent>(out var randomTargetComponent) && Random.Range(0, 1) <= randomTargetComponent.Chance)
            {
                IEntity randomTarget = GetRandomEnemy(attackerEntity);
                targetEntity = randomTarget;
            }
            
            _eventBus.RaiseEvent(new AttackEvent(attackerEntity, targetEntity));

            if (targetEntity.Get<StatsComponent>().CurrentHealth > 0
                && attackerEntity.TryGet<ProtectedAttackComponent>(out var _) == false
                && attackerEntity.Get<StatsComponent>().CurrentHealth > 0)
            {
                _eventBus.RaiseEvent(new AttackEvent(targetEntity, attackerEntity));
            }

            if (attackerEntity.TryGet<RandomAttackComponent>(out var randomAttackComponent) 
                && attackerEntity.Get<StatsComponent>().CurrentHealth > 0)
            {
                IEntity randomTarget = GetRandomEnemy(attackerEntity);
                _eventBus.RaiseEvent(new TakeDamageEvent(randomTarget, attackerEntity, randomAttackComponent.Damage));
                _eventBus.RaiseEvent(new SpecialAbilityEvent(attackerEntity));
            }
            
            if (attackerEntity.TryGet<FrozeAttackComponent>(out var frozeAttackComponent))
            {
                if (targetEntity.TryGet<FrozenTag>(out var frozenTag))
                {
                    frozenTag.TurnsLeft += frozeAttackComponent.TurnsDuration;
                    targetEntity.Set(frozenTag);
                }
                else
                {
                    targetEntity.Add(new FrozenTag(frozeAttackComponent.TurnsDuration));
                }
            }
            
            Finish();
        }
        
        private IEntity GetRandomEnemy(IEntity entity)
        {
            Team team = entity.Get<TeamComponent>().Team;
            return team == Team.Red ? _teamsSetup.BlueTeam.GetRandom() : _teamsSetup.RedTeam.GetRandom();
        }
    }
}