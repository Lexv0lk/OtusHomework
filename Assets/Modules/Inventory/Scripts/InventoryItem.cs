using System;
using Lessons.MetaGame.Inventory.Components;
using UnityEngine;

namespace Lessons.MetaGame.Inventory
{
    [Serializable]
    public sealed class InventoryItem
    {
        public string Name => this.name;
        public InventoryItemFlags Flags => this.flags;
        public InventoryItemMetadata Metadata => this.metadata;

        [SerializeField]
        private string name;

        [SerializeField]
        private InventoryItemFlags flags;

        [SerializeField]
        private InventoryItemMetadata metadata;

        [SerializeReference]
        private IInventoryItemComponent[] components;

        public InventoryItem(
            string name,
            InventoryItemFlags flags,
            InventoryItemMetadata metadata,
            params IInventoryItemComponent[] components
        )
        {
            this.name = name;
            this.flags = flags;
            this.metadata = metadata;
            this.components = components;
        }

        public T GetComponent<T>() where T : IInventoryItemComponent
        {
            foreach (var component in this.components)
            {
                if (component is T tComponent)
                {
                    return tComponent;
                }
            }

            throw new Exception($"Component of type {typeof(T).Name} is not found!");
        }

        public bool TryGetComponent<T>(out T component) where T : IInventoryItemComponent
        {
            foreach (var comp in this.components)
            {
                if (comp is T tComponent)
                {
                    component = tComponent;
                    return true;
                }
            }

            component = default;
            return false;
        }

        public InventoryItem Clone()
        {
            var count = this.components.Length;
            var components = new IInventoryItemComponent[count];

            for (int i = 0; i < count; i++)
            {
                var component = this.components[i];
                
                if (component is ICloneable cloneable)
                {
                    component = (IInventoryItemComponent)cloneable.Clone();
                }

                components[i] = component;
            }
            
            return new InventoryItem(this.name, this.flags, this.metadata, components);
        }
    }
}