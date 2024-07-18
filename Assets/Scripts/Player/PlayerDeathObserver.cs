using ShootEmUp.Characters;
using ShootEmUp.GameStates;
using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class PlayerDeathObserver
    {
        [SerializeField] private Character _player;
        [SerializeField] private GameStateController _gameStateController;

        private void OnEnable()
        {
            _player.Died += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _player.Died -= OnCharacterDeath;
        }

        private void OnCharacterDeath(Character character)
        {
            _gameStateController.FinishGame();
        }
    }
}