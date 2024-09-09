using System.Collections.Generic;
using System.Linq;
using Configs;
using Controllers;
using Entities;
using Entities.Components;
using EventBus.Events;
using Models;
using UI;
using UnityEngine;
using Utils;

namespace Pipeline.Tasks.Logic
{
    public class PlayerInputTask : EventTask
    {
        private readonly PlayerInputController _playerInputController;
        private readonly CurrentTurn _currentTurn;
        private readonly EventBus.EventBus _eventBus;
        private readonly List<IEntity> _entities = new();
        
        public PlayerInputTask(PlayerInputController playerInputController, TeamsSetup teamsSetup,
            CurrentTurn currentTurn, EventBus.EventBus eventBus)
        {
            _playerInputController = playerInputController;
            _currentTurn = currentTurn;
            _eventBus = eventBus;

            foreach (var entity in teamsSetup.RedTeam.GetAllNonIteratable())
                _entities.Add(entity);
            
            foreach (var entity in teamsSetup.BlueTeam.GetAllNonIteratable())
                _entities.Add(entity);
        }

        protected override void OnRun()
        {
            _playerInputController.Clicked += OnHeroClicked;
        }

        protected override void OnFinish()
        {
            _playerInputController.Clicked -= OnHeroClicked;
        }

        private void OnHeroClicked(HeroView view)
        {
            var entity = GetConnectedEntity(view);
            Team entityTeam = entity.Get<TeamComponent>().Team;
            
            if (_currentTurn.EntityInTurn.Get<TeamComponent>().Team == entityTeam)
                return;
            
            _eventBus.RaiseEvent(new TurnAttackEvent(_currentTurn.EntityInTurn, entity));
            
            Finish();
        }

        private IEntity GetConnectedEntity(HeroView view)
        {
            foreach (var entity in _entities)
            {
                if (entity.Get<HeroPresentationComponent>().View == view)
                    return entity;
            }

            return null;
        }
    }
}