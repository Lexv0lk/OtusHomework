using System;
using Leopotam.EcsLite;

namespace Client.Components
{
    [Serializable]
    public struct SourceEntity
    {
        public EcsPackedEntity Value;
    }
}