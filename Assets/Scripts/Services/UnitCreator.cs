using Client.Components;
using Client.Components.Installers;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Client.Services
{
    public class UnitCreator : MonoBehaviour
    {
        [Button]
        private void Create(UnitInstaller unit, Vector3 position)
        {
            Entity entity = unit.GetComponent<Entity>();
            var ecsStartup = EcsStartup.Instance;

            ecsStartup.CreateEntity(EcsWorlds.EVENTS)
                .Add(new SpawnRequest())
                .Add(new Position() { Value = position })
                .Add(new Prefab() { Value = entity })
                .Add(new Rotation() { Value = Quaternion.identity});
        }

        [Button]
        private void Create(UnitInstaller unit, Transform point)
        {
            Create(unit, point.position);
        }
    }
}