using System;
using UnityEngine;

namespace Lessons.MetaGame.Inventory.Components
{
    public class EquipmentHealthChanger : IInventoryItemComponent, ICloneable
    {
        [SerializeField] private int _healthAddition;

        public int HealthAddition => _healthAddition;
        
        public EquipmentHealthChanger() {}

        public EquipmentHealthChanger(int healthAddition)
        {
            _healthAddition = healthAddition;
        }
        
        public object Clone()
        {
            return new EquipmentHealthChanger(_healthAddition);
        }
    }
}