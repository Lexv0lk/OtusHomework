using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class UnitManager
    {
        private Transform container;
        private HashSet<Unit> sceneUnits = new();

        public UnitManager(Transform container, List<Unit> units)
        {
            this.container = container;
            this.sceneUnits = new HashSet<Unit>(units);
        }

        public void SetContainer(Transform container)
        {
            this.container = container;
        }

        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            var unit = Object.Instantiate(prefab, position, rotation, this.container);
            this.sceneUnits.Add(unit);
            return unit;
        }

        public void DestroyUnit(Unit unit)
        {
            if (this.sceneUnits.Remove(unit))
            {
                Object.Destroy(unit.gameObject);
            }
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            return this.sceneUnits;
        }
    }
}