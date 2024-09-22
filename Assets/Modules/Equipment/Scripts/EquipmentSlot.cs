using System;
using Lessons.MetaGame.Inventory;
using Lessons.MetaGame.Inventory.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Equipment
{
    [Serializable]
    public class EquipmentSlot
    {
        [SerializeField] private EquipmentType _equipmentType;
        [ShowInInspector] private InventoryItem _currentItem;

        public EquipmentType EquipmentType => _equipmentType;
        public InventoryItem CurrentItem => _currentItem;

        public bool CanEquip(InventoryItem item)
        {
            if (!item.TryGetComponent<InventoryItemEquipmentComponent>(out var equipmentComponent))
                return false;
            
            if (equipmentComponent.EquipmentType != _equipmentType)
                return false;

            return true;
        }

        public void Equip(InventoryItem item)
        {
            _currentItem = item;
        }
        
        public void Unequip()
        {
            _currentItem = null;
        }
    }
}