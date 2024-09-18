using System;

namespace Rewards
{
    public interface IReward
    {
        
    }

    [Serializable]
    public struct GoldReward
    {
        public int Amount;
    }

    [Serializable]
    public struct RubyReward
    {
        public int Amount;
    }
}