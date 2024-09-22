using Lessons.MetaGame.Inventory;
using Lessons.MetaGame.Inventory.Components;

namespace Modules.Equipment.Observers
{
    public class EquipmentBuffObserver
    {
        private readonly Player _player;
        private readonly EquipmentList _equipmentList;

        public EquipmentBuffObserver(Player player, EquipmentList equipmentList)
        {
            _player = player;
            _equipmentList = equipmentList;

            _equipmentList.ItemEquipped += OnItemEquipped;
            _equipmentList.ItemUnequipped += OnItemUnequipped;
        }
        
        private void OnItemEquipped(InventoryItem item)
        {
            if (item.TryGetComponent<EquipmentDamageChanger>(out var damageChanger))
                _player.damage += damageChanger.DamageAddition;
            
            if (item.TryGetComponent<EquipmentHealthChanger>(out var healthChanger))
                _player.hitPoints += healthChanger.HealthAddition;
            
            if (item.TryGetComponent<EquipmentSpeedChanger>(out var speedChanger))
                _player.speed += speedChanger.SpeedAddition;
        }

        private void OnItemUnequipped(InventoryItem item)
        {
            if (item.TryGetComponent<EquipmentDamageChanger>(out var damageChanger))
                _player.damage -= damageChanger.DamageAddition;
            
            if (item.TryGetComponent<EquipmentHealthChanger>(out var healthChanger))
                _player.hitPoints -= healthChanger.HealthAddition;
            
            if (item.TryGetComponent<EquipmentSpeedChanger>(out var speedChanger))
                _player.speed -= speedChanger.SpeedAddition;
        }

        ~EquipmentBuffObserver()
        {
            _equipmentList.ItemEquipped -= OnItemEquipped;
            _equipmentList.ItemUnequipped -= OnItemUnequipped;
        }
    }
}