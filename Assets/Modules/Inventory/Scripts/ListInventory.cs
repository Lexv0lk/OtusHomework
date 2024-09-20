using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Lessons.MetaGame.Inventory
{
    public sealed class ListInventory
    {
        [ShowInInspector, ReadOnly]
        private List<InventoryItem> items;

        public event Action<InventoryItem> ItemAdded;
        public event Action<InventoryItem> ItemRemoved;

        public ListInventory(params InventoryItem[] items)
        {
            this.items = new List<InventoryItem>(items);
        }

        public void Setup(params InventoryItem[] items)
        {
            this.items = new List<InventoryItem>(items);
        }

        public void AddItem(InventoryItem item)
        {
            if (!this.items.Contains(item))
            {
                this.items.Add(item);
                ItemAdded?.Invoke(item);
            }
        }
        
        public void RemoveItem(InventoryItem item)
        {
            if (this.items.Remove(item))
            {
                ItemRemoved?.Invoke(item);
            }
        }

        public void RemoveItems(string name, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.RemoveItem(name);
            }
        }

        public void RemoveItem(string name)
        {
            if (this.FindItem(name, out var item))
            {
                this.RemoveItem(item);
            }
        }

        public List<InventoryItem> GetItems()
        {
            return this.items.ToList();
        }

        public bool FindItem(string name, out InventoryItem result)
        {
            foreach (var inventoryItem in this.items)
            {
                if (inventoryItem.Name == name)
                {
                    result = inventoryItem;
                    return true;
                }
            }
            
            result = null;
            return false;
        }

        public int GetCount(string item)
        {
            return this.items.Count(it => it.Name == item);
        }
    }
}