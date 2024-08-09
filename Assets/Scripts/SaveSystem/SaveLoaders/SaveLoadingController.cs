using System.Collections.Generic;
using DI.Contexts;

namespace SaveSystem.SaveLoaders
{
    public class SaveLoadingController
    {
        private readonly GameRepository _gameRepository;
        private readonly GameContext _gameContext;
        private readonly List<ISaveLoader> _saveLoaders;

        public SaveLoadingController(GameRepository gameRepository, GameContext gameContext, List<ISaveLoader> saveLoaders)
        {
            _gameRepository = gameRepository;
            _gameContext = gameContext;
            _saveLoaders = saveLoaders;
        }

        public void Load()
        {
            _gameRepository.LoadState();

            foreach (var saveLoader in _saveLoaders)
                saveLoader.LoadState(_gameRepository, _gameContext);
        }

        public void Save()
        {
            foreach (var saveLoader in _saveLoaders)
                saveLoader.SaveState(_gameRepository, _gameContext);
            
            _gameRepository.SaveState();
        }
    }
}