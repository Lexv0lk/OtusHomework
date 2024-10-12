using System;
using Client.Common;

namespace Client.Components
{
    [Serializable]
    public struct AttackData
    {
        public float Range;
        public float ReloadTime;
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

    [Serializable]
    public struct Reload
    {
        public float TimeLeft;
    }
}