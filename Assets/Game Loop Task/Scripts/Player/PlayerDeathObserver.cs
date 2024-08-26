using ShootEmUp.Characters;
using ShootEmUp.GameStates;
using ShootEmUp.Managers;
using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class PlayerDeathObserver : MonoBehaviour
    {
        [SerializeField] private Character _player;
        [SerializeField] private GameStateController _gameStateController;

        private void OnEnable()
        {
            _player.HitPointsComponent.HitPointsEnded += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _player.HitPointsComponent.HitPointsEnded -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject character)
        {
            _gameStateController.FinishGame();
        }
    }
}