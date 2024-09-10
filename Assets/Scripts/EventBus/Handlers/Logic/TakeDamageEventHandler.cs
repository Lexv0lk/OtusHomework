using System.Collections.Generic;
using Configs;
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
        private readonly LobbySettings _lobbySettings;

        public TakeDamageEventHandler(EventBus eventBus, TeamsSetup teamsSetup, LobbySettings lobbySettings) : base(eventBus)
        {
            _teamsSetup = teamsSetup;
            _lobbySettings = lobbySettings;
        }

        protected override void OnHandleEvent(TakeDamageEvent evt)
        {
            var defenderStats = evt.Target.Get<StatsComponent>();
            
            int lastDefenderHealth = defenderStats.CurrentHealth;
            int newDefenderHealth;

            if (evt.Target.TryGet<ShieldComponent>(out var shieldComponent) && shieldComponent.Used == false)
            {
                newDefenderHealth = lastDefenderHealth;
                shieldComponent.Used = true;
                evt.Target.Set(shieldComponent);
            }
            else
            {
                newDefenderHealth = Mathf.Max(0, defenderStats.CurrentHealth - evt.Damage);
            }
            
            int damageDealed = lastDefenderHealth - newDefenderHealth;
            EntityDebug.Log(evt.Target, $"got {damageDealed} damage");
            evt.Target.Set(new StatsComponent(defenderStats.Attack, newDefenderHealth));

            float lastHealthPart = (float)lastDefenderHealth / defenderStats.MaxHealth;
            float newHealthPart = (float)newDefenderHealth / defenderStats.MaxHealth;

            if (lastHealthPart >= _lobbySettings.LowHealthThreshold &&
                newHealthPart < _lobbySettings.LowHealthThreshold && newHealthPart > 0)
                EventBus.RaiseEvent(new LowHealthEvent(evt.Target));

            if (newDefenderHealth <= 0)
            {
                EventBus.RaiseEvent(new DestroyEvent(evt.Target));
            }
            else if (evt.Target.TryGet<MassAttackComponent>(out var massAttackComponent))
            {
                Team targetTeam = evt.Target.Get<TeamComponent>().Team;
                EventBus.RaiseEvent(new SpecialAbilityEvent(evt.Target));
                DealMassAttack(targetTeam == Team.Red ? _teamsSetup.BlueTeam.GetAllNonIteratable() : _teamsSetup.RedTeam.GetAllNonIteratable(),
                    evt.Target, massAttackComponent.Damage);
            }

            if (evt.Source.TryGet<VampireAttackComponent>(out var vampireAttackComponent) && Random.Range(0, 1) <= vampireAttackComponent.Chance)
            {
                var sourceStats = evt.Source.Get<StatsComponent>();

                if (sourceStats.CurrentHealth > 0)
                    EventBus.RaiseEvent(new HealEvent(evt.Source, damageDealed));
            }
        }
        
        private void DealMassAttack(IEnumerable<IEntity> entities, IEntity source, int damage)
        {
            foreach (var entity in entities)
                EventBus.RaiseEvent(new TakeDamageEvent(entity, source, damage));
        }
    }
}