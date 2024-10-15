using System.Collections.Generic;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Services
{
    public class EndGameController
    {
        private readonly EntityManager _entityManager;
        private readonly GameObject _endGameView;
        private readonly HashSet<Entity> _bases;

        public EndGameController(EntityManager entityManager, GameObject endGameView, IEnumerable<Entity> bases)
        {
            _entityManager = entityManager;
            _endGameView = endGameView;
            _bases = new HashSet<Entity>(bases);
            
            _entityManager.Destroying += OnEntityDestroying;
        }

        private void OnEntityDestroying(Entity obj)
        {
            if (_bases.Contains(obj))
            {
                _endGameView.SetActive(true);
                Time.timeScale = 0;
            }
        }

        ~EndGameController()
        {
            _entityManager.Destroying -= OnEntityDestroying;
        }
    }
}