using System.Collections.Generic;
using DI.Contexts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class UnitManager : IGameService
    {
        private Transform container;
        private HashSet<Unit> sceneUnits = new();

        public UnitManager(Transform container, IEnumerable<Unit> units)
        {
            this.container = container;
            this.sceneUnits = new HashSet<Unit>(units);
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

        public void DestroyAllUnits()
        {
            foreach (var unit in this.sceneUnits)
                Object.Destroy(unit.gameObject);
            
            this.sceneUnits.Clear();
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            return this.sceneUnits;
        }
    }
}