using System.Collections.Generic;
using Entities;
using Entities.Components;
using EventBus.Events;
using Models;
using UnityEngine;
using Utils;

namespace EventBus.Handlers.Logic
{
    public class TakeDamageEventHandler : BaseHandler<TakeDamageEvent>
    {
        private readonly TeamsSetup _teamsSetup;

        public TakeDamageEventHandler(EventBus eventBus, TeamsSetup teamsSetup) : base(eventBus)
        {
            _teamsSetup = teamsSetup;
        }

        protected override void OnHandleEvent(TakeDamageEvent evt)
        {
            var defenderStats = evt.Target.Get<StatsComponent>();
            
            int lastDefenderHealth = defenderStats.Health;
            int newDefenderHealth;

            if (evt.Target.TryGet<ShieldComponent>(out var shieldComponent) && shieldComponent.Used == false)
            {
                newDefenderHealth = lastDefenderHealth;
                shieldComponent.Used = true;
                evt.Target.Set(shieldComponent);
            }
            else
            {
                newDefenderHealth = Mathf.Max(0, defenderStats.Health - evt.Damage);
            }
            
            int damageDealed = lastDefenderHealth - newDefenderHealth;
            
            evt.Target.Set(new StatsComponent(defenderStats.Attack, newDefenderHealth));

            if (newDefenderHealth <= 0)
            {
                EventBus.RaiseEvent(new DestroyEvent(evt.Target));
            }
            else if (evt.Target.TryGet<MassAttackComponent>(out var massAttackComponent))
            {
                Team targetTeam = evt.Target.Get<TeamComponent>().Team;
                DealMassAttack(targetTeam == Team.Red ? _teamsSetup.BlueTeam.GetAllNonIteratable() : _teamsSetup.RedTeam.GetAllNonIteratable(),
                    evt.Target, massAttackComponent.Damage);
            }

            if (evt.Source.TryGet<VampireAttackComponent>(out var vampireAttackComponent) && Random.Range(0, 1) <= vampireAttackComponent.Chance)
            {
                var sourceStats = evt.Source.Get<StatsComponent>();
                sourceStats.Health += damageDealed;
                evt.Source.Set(sourceStats);
            }
        }
        
        private void DealMassAttack(IEnumerable<IEntity> entities, IEntity source, int damage)
        {
            foreach (var entity in entities)
                EventBus.RaiseEvent(new TakeDamageEvent(entity, source, damage));
        }
    }
}