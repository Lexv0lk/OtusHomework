using System.Collections.Generic;
using Lessons.MetaGame.Inventory;
using Modules.Equipment.Observers;
using Sirenix.OdinInspector;

namespace Modules.Equipment
{
    public class EquipmentList
    {
        [ShowInInspector, ReadOnly] private readonly List<EquipmentSlot> _slots = new();
        private readonly List<IEquipmentObserver> _observers = new();
        
        public EquipmentList() {}

        public EquipmentList(List<EquipmentSlot> slots)
        {
            _slots = slots;
        }

        public void AddObserver(IEquipmentObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IEquipmentObserver observer)
        {
            _observers.Remove(observer);
        }
        
        public bool TryEquip(InventoryItem item)
        {
            foreach (var slot in _slots)
            {
                if (slot.CanEquip(item))
                {
                    if (slot.CurrentItem != null)
                        OnItemUnequipped(slot.CurrentItem);
                    
                    slot.Equip(item);
                    OnItemEquipeed(item);
                    return true;
                }
            }

            return false;
        }

        public bool Unequip(InventoryItem item)
        {
            foreach (var slot in _slots)
            {
                if (slot.CurrentItem == item)
                {
                    OnItemUnequipped(item);
                    slot.Unequip();
                    return true;
                }
            }

            return false;
        }

        public bool Unequip(EquipmentType type)
        {
            foreach (var slot in _slots)
            {
                if (slot.EquipmentType == type && slot.CurrentItem != null)
                {
                    OnItemUnequipped(slot.CurrentItem);
                    slot.Unequip();
                    return true;
                }
            }

            return false;
        }

        private void OnItemUnequipped(InventoryItem item)
        {
            foreach (var observer in _observers)
                observer.OnItemEquipped(item);
        }
        
        private void OnItemEquipeed(InventoryItem item)
        {
            foreach (var observer in _observers)
                observer.OnItemUnequipped(item);
        }
    }
}