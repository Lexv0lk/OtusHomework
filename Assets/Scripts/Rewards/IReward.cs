using System;

namespace Rewards
{
    public interface IReward
    {
        void Accept(IRewardVisitor visitor);
    }

    [Serializable]
    public struct GoldReward : IReward
    {
        public int Amount;

        public void Accept(IRewardVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [Serializable]
    public struct RubyReward : IReward
    {
        public int Amount;

        public void Accept(IRewardVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}