using Lessons.MetaGame.Inventory;

namespace Modules.Equipment.Observers
{
    public interface IEquipmentObserver
    {
        void OnItemEquipped(InventoryItem item);
        void OnItemUnequipped(InventoryItem item);
    }
}