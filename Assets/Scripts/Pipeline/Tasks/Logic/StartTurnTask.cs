using System.Collections.Generic;
using Configs;
using Entities;
using EventBus.Events;
using Models;
using UnityEngine;
using Utils;

namespace Pipeline.Tasks.Logic
{
    public class StartTurnTask : EventTask
    {
        private readonly CurrentTurn _currentTurn;
        private readonly EventBus.EventBus _eventBus;
        private readonly IteratableList<IteratableList<SOEntity>> _teams;

        public StartTurnTask(CurrentTurn currentTurn, TeamsConfig teamsConfig, EventBus.EventBus eventBus)
        {
            _currentTurn = currentTurn;
            _eventBus = eventBus;

            var redTeam = new IteratableList<SOEntity>(teamsConfig.RedTeam);
            var blueTeam = new IteratableList<SOEntity>(teamsConfig.BlueTeam);
            _teams = new IteratableList<IteratableList<SOEntity>>(new List<IteratableList<SOEntity>> {redTeam, blueTeam});
        }
        
        protected override void OnRun()
        {
            var currentTeam = _teams.GetNext();
            var currentEntity = currentTeam.GetNext();
            _currentTurn.EntityInTurn = currentEntity;
            
            _eventBus.RaiseEvent(new StartTurnEvent(currentEntity));
            
            Finish();
        }
    }
}