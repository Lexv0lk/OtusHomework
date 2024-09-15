using Lessons.MetaGame.Inventory;
using Lessons.MetaGame.Inventory.Components;

namespace Modules.Equipment.Observers
{
    public class EquipmentBuffObserver : IEquipmentObserver
    {
        private readonly Player _player;

        public EquipmentBuffObserver(Player player)
        {
            _player = player;
        }
        
        public void OnItemEquipped(InventoryItem item)
        {
            if (item.TryGetComponent<EquipmentDamageChanger>(out var damageChanger))
                _player.damage += damageChanger.DamageAddition;
            
            if (item.TryGetComponent<EquipmentHealthChanger>(out var healthChanger))
                _player.hitPoints += healthChanger.HealthAddition;
            
            if (item.TryGetComponent<EquipmentSpeedChanger>(out var speedChanger))
                _player.speed += speedChanger.SpeedAddition;
        }

        public void OnItemUnequipped(InventoryItem item)
        {
            if (item.TryGetComponent<EquipmentDamageChanger>(out var damageChanger))
                _player.damage -= damageChanger.DamageAddition;
            
            if (item.TryGetComponent<EquipmentHealthChanger>(out var healthChanger))
                _player.hitPoints -= healthChanger.HealthAddition;
            
            if (item.TryGetComponent<EquipmentSpeedChanger>(out var speedChanger))
                _player.speed -= speedChanger.SpeedAddition;
        }
    }
}