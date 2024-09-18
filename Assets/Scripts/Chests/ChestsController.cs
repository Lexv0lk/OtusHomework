using System.Collections.Generic;
using System.Linq;
using Chests.Configs;
using DI.Contexts;
using Time;

namespace Chests
{
    public class ChestsController : IGameService
    {
        private readonly ServerTimeController _serverTimeController;
        
        private List<Chest> _currentChests = new();

        public IEnumerable<Chest> CurrentChests => _currentChests;

        public ChestsController(ServerTimeController serverTimeController)
        {
            _serverTimeController = serverTimeController;
        }

        public void SetupChests(IEnumerable<Chest> chests)
        {
            _currentChests = chests.ToList();
        }
        
        public void AddChest(ChestConfig config)
        {
            _currentChests.Add(new Chest(config, _serverTimeController.GetCurrentTime()));            
        }
    }
}