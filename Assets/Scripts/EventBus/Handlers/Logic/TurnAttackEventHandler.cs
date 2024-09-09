using Entities;
using Entities.Components;
using EventBus.Events;
using Models;
using UnityEngine;
using Utils;

namespace EventBus.Handlers.Logic
{
    public class TurnAttackEventHandler : BaseHandler<TurnAttackEvent>
    {
        private readonly TeamsSetup _teamsSetup;

        public TurnAttackEventHandler(EventBus eventBus, TeamsSetup teamsSetup) : base(eventBus)
        {
            _teamsSetup = teamsSetup;
        }

        protected override void OnHandleEvent(TurnAttackEvent evt)
        {
            if (evt.Source.TryGet<RandomTargetComponent>(out var randomTargetComponent) && Random.Range(0, 1) <= randomTargetComponent.Chance)
            {
                IEntity randomTarget = GetRandomEnemy(evt.Source);
                EventBus.RaiseEvent(new AttackEvent(evt.Source, randomTarget));
            }
            else
            {
                EventBus.RaiseEvent(new AttackEvent(evt.Source, evt.Target));
            }
            
            if (evt.Target.Get<StatsComponent>().Health > 0 && evt.Source.TryGet<ProtectedAttackComponent>(out var _) == false)
                EventBus.RaiseEvent(new AttackEvent(evt.Target, evt.Source));

            if (evt.Source.TryGet<RandomAttackComponent>(out var randomAttackComponent))
            {
                IEntity randomTarget = GetRandomEnemy(evt.Source);
                EventBus.RaiseEvent(new TakeDamageEvent(randomTarget, evt.Source, randomAttackComponent.Damage));
            }
        }
        
        protected IEntity GetRandomEnemy(IEntity entity)
        {
            Team team = entity.Get<TeamComponent>().Team;
            return team == Team.Red ? _teamsSetup.BlueTeam.GetRandom() : _teamsSetup.RedTeam.GetRandom();
        }
    }
}