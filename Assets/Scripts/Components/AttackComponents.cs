using System;
using Client.Common;

namespace Client.Components
{
    [Serializable]
    public struct AttackData
    {
        public float Range;
        public float Rate;
        public float Damage;
    }

    [Serializable]
    public struct Health
    {
        public float Value;
    }

    [Serializable]
    public struct TeamData
    {
        public Team Value;
    }
}