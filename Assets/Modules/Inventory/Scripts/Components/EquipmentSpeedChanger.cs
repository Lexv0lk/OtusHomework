using System;
using UnityEngine;

namespace Lessons.MetaGame.Inventory.Components
{
    public class EquipmentSpeedChanger : IInventoryItemComponent, ICloneable
    {
        [SerializeField] private float _speedAddition;

        public float SpeedAddition => _speedAddition;
        
        public EquipmentSpeedChanger() {}

        public EquipmentSpeedChanger(float speedAddition)
        {
            _speedAddition = speedAddition;
        }
        
        public object Clone()
        {
            return new EquipmentSpeedChanger(_speedAddition);
        }
    }
}