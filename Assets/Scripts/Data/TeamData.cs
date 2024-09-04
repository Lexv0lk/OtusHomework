using Structs;
using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct TeamData : IComponentData
    {
        public Team Value;
    }
}