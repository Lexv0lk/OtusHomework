using System;
using UnityEngine;

namespace Client.Components
{
    [Serializable]
    public struct TransformView
    {
        public Transform Value;
    }

    [Serializable]
    public struct AnimatorView
    {
        public Animator Animator;
    }
}