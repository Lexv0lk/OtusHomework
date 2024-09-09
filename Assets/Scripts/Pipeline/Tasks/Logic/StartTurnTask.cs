using System.Collections.Generic;
using Configs;
using Entities;
using Entities.Components;
using EventBus.Events;
using Models;
using Utils;

namespace Pipeline.Tasks.Logic
{
    public class StartTurnTask : EventTask
    {
        private readonly CurrentTurn _currentTurn;
        private readonly EventBus.EventBus _eventBus;
        private readonly IteratableList<IteratableList<IEntity>> _teams;

        public StartTurnTask(CurrentTurn currentTurn, TeamsSetup teamsSetup, EventBus.EventBus eventBus)
        {
            _currentTurn = currentTurn;
            _eventBus = eventBus;
            
            _teams = new IteratableList<IteratableList<IEntity>>();
            _teams.Add(teamsSetup.RedTeam);
            _teams.Add(teamsSetup.BlueTeam);
        }
        
        protected override void OnRun()
        {
            var currentTeam = _teams.GetNext();
            var currentEntity = currentTeam.GetNext();
            _currentTurn.EntityInTurn = currentEntity;

            while (_currentTurn.EntityInTurn.TryGet<FrozenTag>(out var frozenTag))
            {
                frozenTag.TurnsLeft--;
                
                if (frozenTag.TurnsLeft == 0)
                    _currentTurn.EntityInTurn.Remove<FrozenTag>();
                else
                    _currentTurn.EntityInTurn.Set(frozenTag);
                
                _currentTurn.EntityInTurn = currentTeam.GetNext();
            }
            
            _eventBus.RaiseEvent(new StartTurnEvent(_currentTurn.EntityInTurn));
            Finish();
        }
    }
}