using System;
using UnityEngine;

namespace Lessons.MetaGame.Inventory.Components
{
    [Serializable]
    public class InventoryItemEquipmentComponent : IInventoryItemComponent, ICloneable
    {
        [SerializeField] private EquipmentType _type;

        public EquipmentType EquipmentType => _type;
        
        public InventoryItemEquipmentComponent()
        {
            
        }

        public InventoryItemEquipmentComponent(EquipmentType type)
        {
            _type = type;
        }
        
        public object Clone()
        {
            return new InventoryItemEquipmentComponent(_type);
        }
    }
}