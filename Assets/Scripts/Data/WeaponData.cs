using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct WeaponData : IComponentData
    {
        public Entity FirePoint;
    }
}