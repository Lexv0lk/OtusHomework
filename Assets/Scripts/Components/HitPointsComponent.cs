using System;
using ShootEmUp.Characters;
using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class HitPointsComponent
    {
        [SerializeField] private int _hitPoints;
        
        public event Action HitPointsEnded;

        public bool IsHitPointsExists()
        {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;

            if (_hitPoints <= 0)
                HitPointsEnded?.Invoke();
        }
    }
}