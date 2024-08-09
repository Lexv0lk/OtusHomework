using System;
using GameEngine.Structs;

namespace SaveSystem.SaveLoaders
{
    [Serializable]
    public struct UnitsSave
    {
        public UnitStateSave[] SavedStates;
    }

    [Serializable]
    public struct ResourcesSave
    {
        public ResourceStateSave[] SavedStates;
    }

    [Serializable]
    public struct ResourceStateSave
    {
        public string Id;
        public int Amount;
    }

    [Serializable]
    public struct UnitStateSave
    {
        public string Type;
        public float3 Position;
        public float3 Rotation;
        public int HitPoints;
    }
}