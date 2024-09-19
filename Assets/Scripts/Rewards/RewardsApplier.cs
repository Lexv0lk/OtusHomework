using Storage;

namespace Rewards
{
    public class RewardsApplier : IRewardVisitor
    {
        private readonly StorageMock _storage;

        public RewardsApplier(StorageMock storage)
        {
            _storage = storage;
        }
        
        public void Visit(GoldReward reward)
        {
            _storage.AddGold(reward.Amount);
        }

        public void Visit(RubyReward reward)
        {
            _storage.AddRuby(reward.Amount);
        }
    }
}