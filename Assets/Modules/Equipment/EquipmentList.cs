﻿using System;
using System.Collections.Generic;
using Lessons.MetaGame.Inventory;
using Lessons.MetaGame.Inventory.Components;
using Sirenix.OdinInspector;

namespace Modules.Equipment
{
    public class EquipmentList
    {
        [ShowInInspector, ReadOnly] private readonly Dictionary<EquipmentType, InventoryItem> _slots = new();

        public event Action<InventoryItem> ItemEquipped;
        public event Action<InventoryItem> ItemUnequipped;

        public EquipmentList() {}

        public EquipmentList(EquipmentType[] types)
        {
            foreach (var type in types)
                _slots.Add(type, null);
        }

        public bool TryEquip(InventoryItem item)
        {
            if (CanEquip(item, out var equipmentComponent))
            {
                var oldItem = _slots[equipmentComponent.EquipmentType];
                
                if (oldItem != null)
                    ItemUnequipped?.Invoke(oldItem);

                _slots[equipmentComponent.EquipmentType] = item;
                ItemEquipped?.Invoke(item);
                return true;
            }

            return false;
        }

        public bool Unequip(InventoryItem item)
        {
            if (item.TryGetComponent<InventoryItemEquipmentComponent>(out var equipmentComponent))
            {
                if (_slots[equipmentComponent.EquipmentType] == item)
                {
                    _slots[equipmentComponent.EquipmentType] = null;
                    ItemUnequipped?.Invoke(item);
                    return true;
                }
            }

            return false;
        }

        public bool Unequip(EquipmentType type)
        {
            if (_slots[type] != null)
            {
                var oldItem = _slots[type];
                _slots[type] = null;
                ItemUnequipped?.Invoke(oldItem);
                return true;
            }

            return false;
        }

        private bool CanEquip(InventoryItem item, out InventoryItemEquipmentComponent equipmentComponent)
        {
            if (!item.TryGetComponent<InventoryItemEquipmentComponent>(out equipmentComponent))
                return false;

            return true;
        }
    }
}