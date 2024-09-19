namespace Rewards
{
    public interface IRewardVisitor
    {
        void Visit(GoldReward reward);
        void Visit(RubyReward reward);
    }
}