using UnityEngine;

namespace ShootEmUp.Characters
{
    public interface IAttackableTarget : IDamageable
    {
        public Vector3 Position { get; }
    }
}