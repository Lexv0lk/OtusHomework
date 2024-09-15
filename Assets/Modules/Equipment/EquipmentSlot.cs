using System;
using Lessons.MetaGame.Inventory;
using Lessons.MetaGame.Inventory.Components;
using UnityEngine;

namespace Modules.Equipment
{
    [Serializable]
    public class EquipmentSlot
    {
        [SerializeField] private EquipmentType _equipmentType;

        private InventoryItem _currentItem;

        public EquipmentType EquipmentType => _equipmentType;
        public InventoryItem CurrentItem => _currentItem;

        public event Action<InventoryItem> Equipped;
        public event Action<InventoryItem> TakenOff;

        public bool TryEquip(InventoryItem newItem)
        {
            if (!newItem.TryGetComponent<InventoryItemEquipmentComponent>(out var equipmentComponent))
                return false;

            if (equipmentComponent.EquipmentType != _equipmentType)
                return false;

            if (_currentItem != null)
                TakenOff?.Invoke(_currentItem);

            _currentItem = newItem;
            Equipped?.Invoke(_currentItem);
            return true;
        }
    }
}