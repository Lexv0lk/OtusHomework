using System.Collections.Generic;
using Lessons.MetaGame.Inventory;
using Modules.Equipment.Observers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Modules.Equipment
{
    public class EquipmentContext : MonoBehaviour
    {
        [ShowInInspector] private EquipmentList _equipment = new();
        [SerializeField] private List<EquipmentSlot> _startSlots = new();
        
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _equipment = new EquipmentList(_startSlots);
            
            var buffObserver = diContainer.Instantiate<EquipmentBuffObserver>();
            _equipment.AddObserver(buffObserver);
        }

        [Button]
        private void Equip(InventoryItemConfig config)
        {
            var prefab = config.item;
            var inventoryItem = prefab.Clone();
            
            if (_equipment.TryEquip(inventoryItem))
                Debug.Log($"{inventoryItem.Name} Equipped");
            else
                Debug.Log($"{inventoryItem.Name} Wasn't equipped");
        }

        [Button]
        private void Unequip(EquipmentType type)
        {
            if (_equipment.Unequip(type))
                Debug.Log($"{type} Unequipped");
            else
                Debug.Log($"{type} wasn't found");
        }
    }
}