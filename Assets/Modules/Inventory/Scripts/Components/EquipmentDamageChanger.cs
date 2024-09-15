using System;
using UnityEngine;

namespace Lessons.MetaGame.Inventory.Components
{
    public class EquipmentDamageChanger : IInventoryItemComponent, ICloneable
    {
        [SerializeField] private int _damageAddition;

        public int DamageAddition => _damageAddition;
        
        public EquipmentDamageChanger() {}

        public EquipmentDamageChanger(int damageAddition)
        {
            _damageAddition = damageAddition;
        }
        
        public object Clone()
        {
            return new EquipmentDamageChanger(_damageAddition);
        }
    }
}