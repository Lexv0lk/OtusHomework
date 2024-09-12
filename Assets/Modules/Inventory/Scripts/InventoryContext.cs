using System;
using System.Collections.Generic;
using Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.MetaGame.Inventory
{
    public sealed class InventoryContext : MonoBehaviour
    {
        [ShowInInspector]
        private readonly ListInventory inventory = new();

        [Button]
        public void AddItem(InventoryItemConfig config)
        {
            var prefab = config.item;
            var inventoryItem = prefab.Clone();
            this.inventory.AddItem(inventoryItem);
        }

        [Button]
        public void RemoveItem(InventoryItemConfig config)
        {
            this.inventory.RemoveItem(config.item.Name);
        }
    }
}