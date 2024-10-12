using System;
using Leopotam.EcsLite;

namespace Client.Components
{
    [Serializable]
    public struct TargetEntity
    {
        public EcsPackedEntity Value;
    }
}