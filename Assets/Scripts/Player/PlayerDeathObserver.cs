using ShootEmUp.Characters;
using ShootEmUp.GameStates;
using Zenject;

namespace ShootEmUp.Player
{
    public sealed class PlayerDeathObserver
    {
        private readonly Character _player;
        private readonly GameStateController _gameStateController;

        [Inject]
        public PlayerDeathObserver(Character player, GameStateController gameStateController)
        {
            _player = player;
            _gameStateController = gameStateController;
            
            _player.Died += OnCharacterDeath;
        }

        ~PlayerDeathObserver()
        {
            _player.Died -= OnCharacterDeath;
        }

        private void OnCharacterDeath(Character character)
        {
            _gameStateController.FinishGame();
        }
    }
}