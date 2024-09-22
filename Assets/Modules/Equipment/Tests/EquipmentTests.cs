using System;
using Lessons.MetaGame.Inventory;
using Lessons.MetaGame.Inventory.Components;
using NUnit.Framework;

namespace Modules.Equipment.Tests
{
    [TestFixture]
    public class EquipmentTests
    {
        [Test]
        public void IfBuffsAreApplied()
        {
            // Arrange
            var equipmentList = GetEmptyList();
            var chest = GetChest();
            int testHealth = 0;
            
            // Act
            Action<InventoryItem> addHealth = (item) =>
            {
                if (item.TryGetComponent<EquipmentHealthChanger>(out var healthChanger))
                    testHealth += healthChanger.HealthAddition;
            };
            equipmentList.ItemEquipped += addHealth;
            equipmentList.TryEquip(chest);
            equipmentList.ItemEquipped -= addHealth;
            
            // Assert
            Assert.AreEqual(testHealth, 100);
        }

        [Test]
        public void IfBuffsAreDisapplied()
        {
            // Arrange
            var equipmentList = GetEmptyList();
            var chest = GetChest();
            int testHealth = 0;
            
            // Act
            Action<InventoryItem> addHealth = (item) =>
            {
                if (item.TryGetComponent<EquipmentHealthChanger>(out var healthChanger))
                    testHealth += healthChanger.HealthAddition;
            };
            
            Action<InventoryItem> removeHealth = (item) =>
            {
                if (item.TryGetComponent<EquipmentHealthChanger>(out var healthChanger))
                    testHealth -= healthChanger.HealthAddition;
            };
            
            equipmentList.ItemEquipped += addHealth;
            equipmentList.ItemUnequipped += removeHealth;
            equipmentList.TryEquip(chest);
            equipmentList.Unequip(chest);
            equipmentList.ItemEquipped -= addHealth;
            equipmentList.ItemUnequipped -= removeHealth;
            
            // Assert
            Assert.AreEqual(testHealth, 0);
        }

        [Test]
        public void IfItemEquipped()
        {
            // Arrange
            var equipmentList = GetEmptyList();
            var chest = GetChest();
            int testHealth = 0;
            
            // Act
            bool equipped = equipmentList.TryEquip(chest);
            
            // Assert
            Assert.IsTrue(equipped);
            Assert.IsTrue(equipmentList.IsEquipped(chest));
            Assert.IsTrue(equipmentList.IsEquipped(EquipmentType.CHEST));
        }
        
        [Test]
        public void WhenItemEquippedOnBusySlot()
        {
            // Arrange
            var equipmentList = GetEmptyList();
            var chest1 = GetChest();
            var chest2 = GetChest();
            
            // Act
            equipmentList.TryEquip(chest1);
            equipmentList.TryEquip(chest2);
            
            // Assert
            Assert.IsTrue(equipmentList.IsEquipped(EquipmentType.CHEST));
            Assert.IsTrue(equipmentList.IsEquipped(chest2));
            Assert.IsFalse(equipmentList.IsEquipped(chest1));
        }
        
        [Test]
        public void IfItemUnequipped()
        {
            // Arrange
            var equipmentList = GetEmptyList();
            var chest = GetChest();
            int testHealth = 0;
            
            // Act
            equipmentList.TryEquip(chest);
            equipmentList.Unequip(chest);
            
            // Assert
            Assert.IsFalse(equipmentList.IsEquipped(chest));
            Assert.IsFalse(equipmentList.IsEquipped(EquipmentType.CHEST));
        }
        
        [Test]
        public void IfItemUnequippedByType()
        {
            // Arrange
            var equipmentList = GetEmptyList();
            var chest = GetChest();
            int testHealth = 0;
            
            // Act
            equipmentList.TryEquip(chest);
            equipmentList.Unequip(EquipmentType.CHEST);
            
            // Assert
            Assert.IsFalse(equipmentList.IsEquipped(chest));
            Assert.IsFalse(equipmentList.IsEquipped(EquipmentType.CHEST));
        }
        
        [Test]
        public void IfItemWithoutEquipmentComponent()
        {
            // Arrange
            var equipmentList = GetEmptyList();
            var item = GetItemWithoutEquipmentComponent();
            
            // Act
            bool equipped = equipmentList.TryEquip(item);
            
            // Assert
            Assert.IsFalse(equipped);
        }
        
        [Test]
        public void WhenUnequippedItemTriedToUnequip()
        {
            // Arrange
            var equipmentList = GetEmptyList();
            var chest = GetChest();
            
            // Act
            bool unequipped = equipmentList.Unequip(chest);
            
            // Assert
            Assert.IsFalse(unequipped);
        }
        
        [Test]
        public void WhenItemTypeIsNotRepresenterInSlots()
        {
            // Arrange
            var equipmentList = GetEmptyListWithoutChest();
            var chest = GetChest();
            
            // Act
            bool equipped = equipmentList.TryEquip(chest);
            
            // Assert
            Assert.IsFalse(equipped);
        }
        
        [Test]
        public void WhenAllSetIsEquipped()
        {
            // Arrange
            var equipmentList = GetEmptyList();
            var helmet = GetHelmet();
            var chest = GetChest();
            var legs = GetLegs();
            var mainHand = GetMainHand();
            var offHand = GetOffHand();
            int testHealth = 0;
            float testSpeed = 0;
            int testDamage = 0;
            
            // Act
            Action<InventoryItem> addHealth = (item) =>
            {
                if (item.TryGetComponent<EquipmentHealthChanger>(out var healthChanger))
                    testHealth += healthChanger.HealthAddition;
            };
            
            Action<InventoryItem> addSpeed = (item) =>
            {
                if (item.TryGetComponent<EquipmentSpeedChanger>(out var speedChanger))
                    testSpeed += speedChanger.SpeedAddition;
            };
            
            Action<InventoryItem> addDamage = (item) =>
            {
                if (item.TryGetComponent<EquipmentDamageChanger>(out var damageChanger))
                    testDamage += damageChanger.DamageAddition;
            };
            
            equipmentList.ItemEquipped += addHealth;
            equipmentList.ItemEquipped += addSpeed;
            equipmentList.ItemEquipped += addDamage;
            
            equipmentList.TryEquip(helmet);
            equipmentList.TryEquip(chest);
            equipmentList.TryEquip(legs);
            equipmentList.TryEquip(mainHand);
            equipmentList.TryEquip(offHand);
            
            equipmentList.ItemEquipped -= addHealth;
            equipmentList.ItemEquipped -= addSpeed;
            equipmentList.ItemEquipped -= addDamage;
            
            // Assert
            Assert.IsTrue(equipmentList.IsEquipped(helmet));
            Assert.IsTrue(equipmentList.IsEquipped(chest));
            Assert.IsTrue(equipmentList.IsEquipped(legs));
            Assert.IsTrue(equipmentList.IsEquipped(mainHand));
            Assert.IsTrue(equipmentList.IsEquipped(offHand));
            Assert.AreEqual(testHealth, 150);
            Assert.AreEqual(testSpeed, 10);
            Assert.AreEqual(testDamage, 15);
        }

        private EquipmentList GetEmptyList()
        {
            return new EquipmentList(EquipmentType.HEAD, EquipmentType.LEGS,
                EquipmentType.CHEST, EquipmentType.OFFHAND, EquipmentType.MAINHAND);
        }

        private EquipmentList GetEmptyListWithoutChest()
        {
            return new EquipmentList(EquipmentType.HEAD, EquipmentType.LEGS,
                EquipmentType.OFFHAND, EquipmentType.MAINHAND);
        }
        
        private InventoryItem GetItemWithoutEquipmentComponent()
        {
            return new InventoryItem("Item", InventoryItemFlags.EQUPPABLE, null);
        }

        private InventoryItem GetHelmet()
        {
            return new InventoryItem("Helmet", InventoryItemFlags.EQUPPABLE, null,
                new InventoryItemEquipmentComponent(EquipmentType.HEAD), new EquipmentHealthChanger(50));
        }
        
        private InventoryItem GetChest()
        {
            return new InventoryItem("Chest", InventoryItemFlags.EQUPPABLE, null,
                new InventoryItemEquipmentComponent(EquipmentType.CHEST), new EquipmentHealthChanger(100));
        }
        
        private InventoryItem GetLegs()
        {
            return new InventoryItem("Legs", InventoryItemFlags.EQUPPABLE, null,
                new InventoryItemEquipmentComponent(EquipmentType.LEGS), new EquipmentSpeedChanger(10));
        }
        
        private InventoryItem GetMainHand()
        {
            return new InventoryItem("MainHand", InventoryItemFlags.EQUPPABLE, null,
                new InventoryItemEquipmentComponent(EquipmentType.MAINHAND), new EquipmentDamageChanger(10));
        }
        
        private InventoryItem GetOffHand()
        {
            return new InventoryItem("OffHand", InventoryItemFlags.EQUPPABLE, null,
                new InventoryItemEquipmentComponent(EquipmentType.OFFHAND), new EquipmentDamageChanger(5));
        }
    }
}