using System;
using Leopotam.EcsLite;

namespace Client.Components
{
    [Serializable]
    public struct Parent
    {
        public EcsPackedEntity Value;
    }
}