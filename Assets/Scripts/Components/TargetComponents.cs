using System;
using Leopotam.EcsLite;

namespace Client.Components
{
    [Serializable]
    public struct Target
    {
        public EcsPackedEntity Value;
    }
}