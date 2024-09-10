using Entities;
using Entities.Components;
using EventBus.Events;
using Models;
using Utils;

namespace Pipeline.Tasks.Logic
{
    public class EndTurnTask : EventTask
    {
        private readonly TeamsSetup _teamsSetup;
        private readonly CurrentTurn _currentTurn;
        private readonly EventBus.EventBus _eventBus;

        public EndTurnTask(TeamsSetup teamsSetup, CurrentTurn currentTurn, EventBus.EventBus eventBus)
        {
            _teamsSetup = teamsSetup;
            _currentTurn = currentTurn;
            _eventBus = eventBus;
        }
        
        protected override void OnRun()
        {
            Team currentTeam = _currentTurn.EntityInTurn.Get<TeamComponent>().Team;
            IteratableList<IEntity> team = currentTeam == Team.Red ? _teamsSetup.RedTeam : _teamsSetup.BlueTeam;

            foreach (var entity in team.GetAllNonIteratable())
            {
                if (entity.TryGet<RandomHealComponent>(out var randomHealComponent))
                {
                    IEntity randomTeammate;

                    do
                    {
                        randomTeammate = team.GetRandom();
                    } while (randomTeammate == entity);
                    
                    _eventBus.RaiseEvent(new HealEvent(randomTeammate, randomHealComponent.Value));
                    _eventBus.RaiseEvent(new SpecialAbilityEvent(entity));
                }
            }
            
            Finish();
        }
    }
}