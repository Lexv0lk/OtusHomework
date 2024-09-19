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
        public int GoldAmount;

        public void Accept(IRewardVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [Serializable]
    public struct RubyReward : IReward
    {
        public int RubyAmount;

        public void Accept(IRewardVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}