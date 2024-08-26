using ShootEmUp.Characters;
using ShootEmUp.Managers;
using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class PlayerDeathObserver : MonoBehaviour
    {
        [SerializeField] private Character _player;
        [SerializeField] private GameManager _gameManager;

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
            _gameManager.FinishGame();
        }
    }
}