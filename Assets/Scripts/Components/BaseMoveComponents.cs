using System;
using UnityEngine;

namespace Client.Components
{
    [Serializable]
    public struct Position
    {
        public Vector3 Value;
    }

    [Serializable]
    public struct Rotation
    {
        public Quaternion Value;
    }

    [Serializable]
    public struct MoveSpeed
    {
        public float BaseSpeed;
        public float CurrentSpeed;
    }

    [Serializable]
    public struct MoveDirection
    {
        public Vector3 Value;
    }
}