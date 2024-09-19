using System;
using System.Collections.Generic;
using System.Linq;
using Chests.Configs;
using DI.Contexts;
using Rewards;
using Sirenix.Utilities;
using Time;
using UniRx;

namespace Chests
{
    public class ChestsController : IGameService
    {
        private readonly ServerTimeController _serverTimeController;
        private readonly RewardsApplier _rewardsApplier;

        private ReactiveCollection<Chest> _currentChests = new();
        private TimeSpan _chestTimeCached;

        public IReadOnlyReactiveCollection<Chest> CurrentChests => _currentChests;

        public ChestsController(ServerTimeController serverTimeController, RewardsApplier rewardsApplier)
        {
            _serverTimeController = serverTimeController;
            _rewardsApplier = rewardsApplier;
        }

        public void SetupChests(IEnumerable<Chest> chests)
        {
            _currentChests.Clear();
            _currentChests.AddRange(chests);
        }
        
        public void Add(ChestConfig config)
        {
            _currentChests.Add(new Chest(config, _serverTimeController.GetCurrentTime()));            
        }

        public bool Remove(Chest chest)
        {
            return _currentChests.Remove(chest);
        }

        public void Open(Chest chest)
        {
            foreach (var reward in chest.Rewards)
                reward.Accept(_rewardsApplier);

            Remove(chest);
        }

        public bool TryGetChestTimeLeft(Chest chest, out TimeSpan timeLeft)
        {
            timeLeft = TimeSpan.MaxValue;
            
            if (_serverTimeController.IsActualTimeReceived == false)
                return false;

            _chestTimeCached = TimeSpan.FromMinutes(chest.CloseDuration);
            DateTime currentTime = _serverTimeController.GetCurrentTime();
            
            if (chest.CreateTime.Add(_chestTimeCached) <= currentTime)
                timeLeft = TimeSpan.Zero;
            else
                timeLeft = chest.CreateTime + _chestTimeCached - currentTime;

            return true;
        }
    }
}