using System;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components
{
    [Serializable]
    public struct BuildingHarmViewData
    {
        public HarmedPosition[] Positions;
        public int CurrentPosIndex;
    }

    [Serializable]
    public struct HarmedPosition
    {
        public float HealthPart;
        public Vector3 Position;
    }
}